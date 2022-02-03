using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBdata.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Studentski_hotel.Data;
using Studentski_hotel.Helper;
using Studentski_hotel.Models.Student;
using cloudscribe.Pagination.Models;
using Studentski_hotel.notHub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace Studentski_hotel.Controllers
{
    [Autorizacija(true, false, false,false)]

    public class StudentController : Controller
    {
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<NotHub> _hubContext;

        public StudentController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ApplicationDbContext _dbContext, IHubContext<NotHub> hubContext, IWebHostEnvironment webHostEnviroment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbContext = _dbContext;
            _hubContext = hubContext;
            _webHostEnvironment = webHostEnviroment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrikazObavijesti(string pretraga, int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            PrikazObavijestiStudent all = new PrikazObavijestiStudent();
            all.obavijesti = dbContext.Obavijests.Where(x => pretraga == null || (x.Naslov.ToLower().StartsWith(pretraga.ToLower())))
            .Select(o => new PrikazObavijestiStudent.Row
            {
                obavijestID = o.ID,
                Naslov = o.Naslov,
                Text = o.Text,
                DatumObj = o.DatumVrijeme,
                ImeRecepcionera = o.Osoblje.Ime + " " + o.Osoblje.Prezime
            }).OrderByDescending(o => o.DatumObj).Skip(ExcludeRecords).Take(pageSize).AsNoTracking().ToList();

            int brojac = dbContext.Obavijests.Count();

            var result = new PagedResult<PrikazObavijestiStudent.Row>
            {
                Data = all.obavijesti,
                PageNumber = pageNumber,
                TotalItems = brojac,
                PageSize = pageSize,
            };

            return View(result);
        }
        public IActionResult PregledObavijesti(int obavijestID)
        {
            var obavijest = dbContext.Obavijests.Include(a => a.Osoblje).Where(a => a.ID == obavijestID).FirstOrDefault();

            PregledObavijestiVM selectedNotification = new PregledObavijestiVM();
            selectedNotification.obavijestID = obavijest.ID;
            selectedNotification.Naslov = obavijest.Naslov;
            selectedNotification.Text = obavijest.Text;
            selectedNotification.ImeRecepcionera = obavijest.Osoblje.Ime + " " + obavijest.Osoblje.Prezime;
            selectedNotification.DatumObj = obavijest.DatumVrijeme;

            return View(selectedNotification);
        }
        public IActionResult PosaljiZahtjev()
        {
            var vrste = dbContext.VrstaZahtjevas.Select(a => new SelectListItem
            {
                Value = a.ID.ToString(),
                Text = a.Naziv.ToString()
            }).ToList();
            PosaljiZahtjevVM model = new PosaljiZahtjevVM();
            model.VrstaZahtjeva = vrste;
            return View(model);
        }
        public async Task<IActionResult> SnimiZahtjev(PosaljiZahtjevVM admir)
        {
            var user = await _userManager.GetUserAsync(User);
            var ugovor = dbContext.Ugovors.Include(a=>a.Student).Include(a=>a.Soba).Where(a => a.Student.KorisnikID==user.Id).FirstOrDefault();
            var Zahtjev = new Zahtjev()
            {
                Datum = DateTime.Now.ToString(),
                VrstaZahtjevaID = admir.VrstaZahtjevaID,
                VrstaStanjaZahtjevaID = 1,
                Text = admir.Text,
                UgovorID=ugovor.ID
                
            };
            dbContext.Add(Zahtjev);
            dbContext.SaveChanges();
            var broj = dbContext.Zahtjevs.Count();
           await _hubContext.Clients.All.SendAsync("SlanjeZahtjeva", ugovor.Student.Ime+" "+ugovor.Student.Prezime,admir.Text, DateTime.Now.ToString(),ugovor.Soba.BrojSobe,broj);

            return Redirect(url: "/Student/PosaljiZahtjev");
        }

        public async Task<IActionResult> PregledLicniPodataka()
        {
            var user = await _userManager.GetUserAsync(User);
            var student = dbContext.Students.Include(a => a.MjestoRodjenja)
                .Include(a => a.TipKandidata)
            .Where(student => student.Korisnik.Id == user.Id).FirstOrDefault();

            LicniPodaciVM personalData = new LicniPodaciVM();
            personalData.Ime = student.Ime;
            personalData.Prezime = student.Prezime;
            personalData.Prebivaliste = student.MjestoRodjenja.Naziv;
            personalData.TipStanara = student.TipKandidata.Naziv;
            personalData.ImeOca = student.ImeOca;
            personalData.BrMobitela = student.Mobitel;
            personalData.SlikaStanara = student.Slika;
            
            personalData.BrLicneKarte = student.JMBG;
            personalData.BrojIndeksa = student.BrojIndeksa;
            personalData.DatumRodjenja = student.DatumRodjenja;

            return View(personalData);

        }

        [BindProperty]
        public DodajSLikuVM NovaSlika { get; set; }
        [HttpPost]
        public async Task<IActionResult> DodajSliku(DodajSLikuVM model)
        {
            var user = await _userManager.GetUserAsync(User);
            var student = dbContext.Students.Include(a => a.MjestoRodjenja)
                .Include(a => a.TipKandidata)
            .Where(student => student.Korisnik.Id == user.Id).FirstOrDefault();
            string uniqueFileName = Image.Upload(model.NovaSlika, _webHostEnvironment, "student");
            student.Slika = uniqueFileName;

            dbContext.SaveChanges();
            return Redirect(url: "/Student/PregledLicniPodataka");
        }


        public async Task<IActionResult> ObrisiSliku(string slika)
        {
            var user = await _userManager.GetUserAsync(User);
            var student = dbContext.Students.Include(a => a.MjestoRodjenja)
                .Include(a => a.TipKandidata)
            .Where(student => student.Korisnik.Id == user.Id).FirstOrDefault();
            Image.Delete(_webHostEnvironment, "student", slika);

            if (student.Slika == slika)
            {
                student.Slika = null;
                dbContext.SaveChanges();
            }

            return Redirect(url: "/Student/PregledLicniPodataka");
        }

        public async Task<IActionResult> PregledUplata(int pageNumber = 1, int pageSize = 2)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var user = await _userManager.GetUserAsync(User);
            var student = dbContext.Students.Where(a => a.KorisnikID == user.Id).FirstOrDefault();

            PregledUplataVM uplateStudenta = new PregledUplataVM();
            uplateStudenta.uplate = dbContext.Uplatas
                .Include(x => x.NacinUplate)
                .Include(x => x.Osoblje)
                .Include(x => x.Ugovor)
                .Where(x => x.Ugovor.StudentID == student.ID)
            .Select(u => new PregledUplataVM.Row
            {
                uplataID = u.ID,
                ImeRecepcionera = u.Osoblje.Ime + " " + u.Osoblje.Prezime,
                NacinUplate = u.NacinUplate.Naziv,
                DatumUplate = u.Datum,
                Iznos = u.Stanje.ToString() + "KM"
            }).OrderBy(u => u.DatumUplate).Skip(ExcludeRecords).Take(pageSize).AsNoTracking().ToList();


            int brojac = dbContext.Uplatas.Where(x => x.Ugovor.StudentID == student.ID).Count();

            var result = new PagedResult<PregledUplataVM.Row>
            {
                Data = uplateStudenta.uplate,
                PageNumber = pageNumber,
                TotalItems = brojac,
                PageSize = pageSize,
            };


            return View(result);
        }


    }

}
