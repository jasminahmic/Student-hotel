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
        public IActionResult StudentPocetna()
        {
            return View();

        }
        public IActionResult PrikazObavijestiStudent(string pretraga)
        {
            PrikazObavijestiStudent all = new PrikazObavijestiStudent();
            all.obavijesti = dbContext.Obavijests.Where(x => pretraga == null || (x.Naslov.ToLower().StartsWith(pretraga.ToLower())))
            .Select(o => new PrikazObavijestiStudent.Row
            {
                obavijestID = o.ID,
                Naslov = o.Naslov,
                Text = o.Text,
                DatumObj = o.DatumVrijeme,
                ImeRecepcionera=o.Osoblje.Ime + " " + o.Osoblje.Prezime
            }).ToList();

            all.obavijesti = all.obavijesti.OrderByDescending(x => x.DatumObj).ToList();

            all.pretraga = pretraga;
            //TempData["all"] = all.obavijesti;
            return View(all);
        }
        public IActionResult PregledObavijestiS(int obavijestID)
        {

            //var user = await _userManager.GetUserAsync(User);
            //var referent = dbContext.Recepcioners.Where(a => a.KorisnikID == user.Id).FirstOrDefault();
            var ob = dbContext.Obavijests.Find(obavijestID);
            //ob.RecepcioerID = referent.Korisnik.Recepcioer.ID;


            PregledObavijestiS po = dbContext.Obavijests.Where(x => x.ID == obavijestID)
                .Select(ob => new PregledObavijestiS { 
            obavijestID = ob.ID,
            Naslov = ob.Naslov,
            Text = ob.Text,
            DatumObj = ob.DatumVrijeme,
            ImeRecepcionera = ob.Osoblje.Ime + " " + ob.Osoblje.Prezime
        }).Single();



            return View(po);
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

    }

}
