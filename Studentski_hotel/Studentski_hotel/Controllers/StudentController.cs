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

namespace Studentski_hotel.Controllers
{
    [Autorizacija(true, false, false,false)]

    public class StudentController : Controller
    {
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private ApplicationDbContext dbContext;
        IHubContext<NotHub> _hubContext;

        public StudentController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ApplicationDbContext _dbContext, IHubContext<NotHub> hubContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbContext = _dbContext;
            _hubContext = hubContext;
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

        public IActionResult PregledLicniPodataka()
        {

            LicniPodaciVM personalData = new LicniPodaciVM();
            personalData.Ime = "Tesni Mesha";
            personalData.Prezime = "Test";
            personalData.Prebivaliste = "Bugojno";
            personalData.TipStanara = "Starija godina";
            personalData.ImeOca = "Hasim";
            personalData.SlikaStanara = "";
            personalData.BrLicneKarte = "prazna";
            personalData.BrLicneKarte = "IB180026";
            personalData.DatumRodjenja = "28/08/2000";

            return View(personalData);

        }

    }

}
