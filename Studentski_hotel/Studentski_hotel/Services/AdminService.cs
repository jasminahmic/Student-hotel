using DBdata.EntityModels;
using Microsoft.AspNetCore.Identity;
using Studentski_hotel.Data;
using Studentski_hotel.Helper;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.Admin;
using Studentski_hotel.Models.AdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Services
{
    public class AdminService : IAdminService
    {
        private ApplicationDbContext _context;
        private IEmailService _emailService;
        private UserManager<Korisnik> _userManager;

        public AdminService(ApplicationDbContext context, IEmailService emailService, UserManager<Korisnik> userManager)
        {
            _context = context;
            _emailService = emailService;
            _userManager = userManager;
        }
        public AdminApiVM PrikazOsoblja()
        {
            var rows = _context.Osobljes.Select(a => new AdminApiVM.Row
            {
                ID = a.ID,
                Ime = a.Ime,
                Prezime = a.Prezime,
                Pozicija = a.Rola.Naziv,
                DatumRodenja = a.DatumRodjenja,
                DatumZaposlenja = a.DatumZaposlenja
            }).ToList();
            AdminApiVM model = new AdminApiVM();
            model.Uposlenici = rows;
            return model;
        }
        public Lista GetList()
        {
            var vm = new Lista();
            var kantoni = _context.Kantons.Select(a => new Lista.Row
            {
                ID = a.ID,
                Naziv = a.Naziv
            }).ToList();
            vm.Kanton = kantoni;
            var spolovi = _context.Pols.Select(x => new Lista.Row
            {
                ID = x.ID,
                Naziv = x.Naziv
            }).ToList();
            vm.Spolovi = spolovi;
            var opstine = _context.Grads.Select(a => new Lista.Row
            {
                ID = a.ID,
                Naziv = a.Naziv,
                KantonID = a.KantonID
            }).ToList();
            vm.Opstina = opstine;
            return vm;
        }
        public async void AddOsobljaAsync(DodajOsobljeVM osoblje)
        {
            var korisnik = new Korisnik();
            korisnik.Email = osoblje.email;
            korisnik.UserName = osoblje.email;
            korisnik.EmailConfirmed = true;
            korisnik.PhoneNumber = osoblje.mobitel;
            var password = "dadadaffjsdvjdgdfdsfddfg";

            IdentityResult result = _userManager.CreateAsync(korisnik, password).Result;
            if (!result.Succeeded)
            {
                //return Content("errors: " + string.Join('|', result.Errors));
                return;

            }
            var lokacija = new Lokacija()
            {
                Adresa = osoblje.Adresa,
                PostanskiBroj = osoblje.PostanskiBroj,
                KantonID = osoblje.KantonID,
                MjestoStanovanjaID = osoblje.MjestoStanovanjaID
            };
            _context.Add(lokacija);
            _context.SaveChanges();
            var osobljee = new Osoblje()
            {
                ID = osoblje.ID,
                Ime = osoblje.Ime,
                Prezime = osoblje.Prezime,
                DatumRodjenja = osoblje.DatumRodjenja.ToString("dd.MM.yyyy"),
                GodinaZaposlenja = osoblje.DatumZaposlenja.ToString("dd.MM.yyyy"),
                LokacijaID = lokacija.ID,
                PolID = osoblje.PolID,
                KorisnikID = osoblje.KorisnikID,
                RolaID = osoblje.TipKorisnika
            };
            _context.Add(osobljee);
            _context.SaveChanges();
            await _emailService.SendEmailAsync(osoblje.email, "Studentski hotel Mostar", "<h1>Poštovani, Vaši pristupni podaci se nalaze u ovom mailu </h1>" +
                    $"<p>Vaši pristupni podaci su :</p>" +
                     $"<p>E-mail : {osoblje.email}</p>" +
                    $"<p>Sifra : {password}</p>");
        }
        public DodajOsobljeVM GetOsoblja(int ID)
        {
            var osoblje = _context.Osobljes.Where(a => a.ID == ID).Select(a => new DodajOsobljeVM
            {
                ID = a.ID,
                Ime = a.Ime,
                Prezime = a.Prezime,
                DatumRodjenja = Convert.ToDateTime(a.DatumRodjenja),
                DatumZaposlenja = Convert.ToDateTime(a.DatumZaposlenja),
                email = a.Korisnik.Email,
                LokacijaID = a.LokacijaID,
                PolID = a.PolID,
            }).FirstOrDefault();
            var lokacija = _context.Lokacijas.FirstOrDefault(a => a.ID == osoblje.LokacijaID);
            osoblje.Adresa = lokacija.Adresa;
            osoblje.PostanskiBroj = lokacija.PostanskiBroj;
            osoblje.MjestoStanovanjaID = lokacija.MjestoStanovanjaID;
            osoblje.KantonID = lokacija.KantonID;
            return osoblje;
        }
        public void EditOsoblja(DodajOsobljeVM vm)
        {
            var osoblje = _context.Osobljes.Where(a => a.ID == vm.ID).FirstOrDefault();
            osoblje.Ime = vm.Ime;
            osoblje.Prezime = vm.Prezime;
            osoblje.DatumRodjenja = vm.DatumRodjenja.ToString("MM/dd/yyyy");
            osoblje.DatumZaposlenja = vm.DatumZaposlenja.ToString("MM/dd/yyyy");
            osoblje.PolID = vm.PolID;
            var lokacija = _context.Lokacijas.FirstOrDefault(a => a.ID == vm.LokacijaID);
            lokacija.Adresa = vm.Adresa;
            lokacija.PostanskiBroj = vm.PostanskiBroj;
            lokacija.KantonID = vm.KantonID;
            lokacija.MjestoStanovanjaID = vm.MjestoStanovanjaID;
            _context.SaveChanges();

        }

    }
}
