using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBdata.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Studentski_hotel.Data;
using Studentski_hotel.Helper;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.Admin;
namespace Studentski_hotel.Controllers
{
    [Autorizacija(false, false, false, true)]
    public class AdminController : Controller
    {

        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private ApplicationDbContext dbContext;
        private IEmailService _emailService;

        public AdminController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ApplicationDbContext _dbContext, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbContext = _dbContext;
            _emailService = emailService;
        }

        public IActionResult AdminPocetna()
        {

            return View();
        }
        public IActionResult PrikazOsoblja(int tipOsoblja)
        {
            
                var stuff = dbContext.Osobljes.Where(a=>a.RolaID == tipOsoblja || tipOsoblja == 0).Select(a => new PrikazOsobljaVM.Row
                {
                    ID = a.ID,
                    Ime = a.Ime,
                    Prezime = a.Prezime,
                    DatumRodjenja = a.DatumRodjenja
                }).ToList();

                PrikazOsobljaVM showStuff = new PrikazOsobljaVM();
                showStuff.Osoblje = stuff;
                return View(showStuff);

        }
        public IActionResult DodajOsoblje(int RecepcionerID)
        {
            List<SelectListItem> kantoni = dbContext.Kantons.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            List<SelectListItem> opstine = dbContext.Grads.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> pol = dbContext.Pols.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            DodajOsobljeVM osoblje;
            if (RecepcionerID > 0)
            {
                osoblje = dbContext.Osobljes.Where(a => a.ID == RecepcionerID).Select(a => new DodajOsobljeVM
                {
                    ID = a.ID,
                    Ime = a.Ime,
                    Prezime = a.Prezime,
                    DatumRodjenja = Convert.ToDateTime(a.DatumRodjenja),
                    PolID = a.PolID,
                    mobitel = a.Korisnik.PhoneNumber,
                    Adresa = a.Lokacija.Adresa,
                    PostanskiBroj= a.Lokacija.PostanskiBroj,
                    MjestoStanovanjaID = a.Lokacija.MjestoStanovanjaID,
                    KantonID = a.Lokacija.KantonID,
                    email = a.Korisnik.Email,
                    KorisnikID = a.KorisnikID,
                    LokacijaID = a.LokacijaID,
                    TipKorisnika = 1,
                    DatumZaposlenja = Convert.ToDateTime(a.DatumZaposlenja),



                }).FirstOrDefault();
               
            }
            else
            {
                osoblje = new DodajOsobljeVM();
            }

            osoblje.Kanton = kantoni;
            osoblje.Pol = pol;
            osoblje.MjestoStanovanja = opstine;
            
            return View(osoblje);
        }

        public async Task<IActionResult> SnimiAsync(DodajOsobljeVM zaposlenik)
        {
            Korisnik korisnik;
            Lokacija lokacija;

            if (zaposlenik.ID == 0)
            {
                korisnik = new Korisnik();
                korisnik.Email = zaposlenik.email;
                korisnik.UserName = zaposlenik.email;
                korisnik.EmailConfirmed = true;
                korisnik.PhoneNumber = zaposlenik.mobitel;
                
                IdentityResult result = _userManager.CreateAsync(korisnik, zaposlenik.password).Result;
                if (!result.Succeeded)
                {
                    return Content("errors: " + string.Join('|', result.Errors));
                }
                lokacija = new Lokacija();
                lokacija.Adresa = zaposlenik.Adresa;
                lokacija.PostanskiBroj = zaposlenik.PostanskiBroj;
                lokacija.MjestoStanovanjaID = zaposlenik.MjestoStanovanjaID;
                lokacija.KantonID = zaposlenik.KantonID;
                dbContext.Add(lokacija);
                dbContext.SaveChanges();

                //slanje maila zaposleniku
            }
            else
            {
                korisnik = dbContext.Korisniks.Where(a => a.Id == zaposlenik.KorisnikID).FirstOrDefault();
                lokacija = dbContext.Lokacijas.Where(a => a.ID == zaposlenik.LokacijaID).FirstOrDefault();
            }


            Osoblje osoblje;
            if (zaposlenik.ID == 0)
            {
                osoblje = new Osoblje();
                osoblje.LokacijaID = lokacija.ID;
                osoblje.RolaID = zaposlenik.TipKorisnika;
                dbContext.Add(osoblje);
                await _emailService.SendEmailAsync(zaposlenik.email, "Studentski hotel Mostar", "<h1>Poštovani, Vaši pristupni podaci se nalaze u ovom mailu </h1>" +
                    $"<p>Vaši pristupni podaci su :</p>"+
                     $"<p>E-mail : {zaposlenik.email}</p>"+
                    $"<p>Sifra : {zaposlenik.password}</p>");

            }
            else
            {
                osoblje = dbContext.Osobljes.Where(a => a.ID == zaposlenik.ID).FirstOrDefault();
                osoblje.LokacijaID = zaposlenik.LokacijaID;
            }
            osoblje.Korisnik = korisnik;
            osoblje.Ime = zaposlenik.Ime;
            osoblje.Prezime = zaposlenik.Prezime;
            osoblje.PolID = zaposlenik.PolID;
            osoblje.DatumRodjenja = zaposlenik.DatumRodjenja.ToString("MM/dd/yyyy");
            osoblje.DatumZaposlenja = zaposlenik.DatumZaposlenja.ToString("MM/dd/yyyy");

            //dbContext.Add(recepcioer);
            dbContext.SaveChanges();

            return Redirect(url: "/Admin/AdminPocetna");
        }
    }
}
