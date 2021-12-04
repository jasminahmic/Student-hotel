using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using DBdata.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Studentski_hotel.Data;
using Studentski_hotel.Helper;
using Studentski_hotel.Models.Recepcija;
using cloudscribe.Pagination.Models;
using Studentski_hotel.Interface;

namespace Studentski_hotel.Controllers
{
    [Autorizacija(false, true, false, false)]
    public class RecepcijaController : Controller
    {
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private ApplicationDbContext dbContext;
        private IEmailService _emailService;



        public RecepcijaController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ApplicationDbContext _dbContext, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbContext = _dbContext;
            _emailService = emailService;
        }

        public IActionResult Index()
        {


            return View();
        }
        public IActionResult EditObavijest(int obavijestID)
        {
            DodajObavijestVM obj = obavijestID == 0 ? new DodajObavijestVM() :
                dbContext.Obavijests.Where(x => x.ID == obavijestID).
                Select(o => new DodajObavijestVM
                {
                    obavijestID = o.ID,
                    Naslov = o.Naslov,
                    Text = o.Text.Trim(),
                    RecepcionerID = o.OsobljeID,
                    datum_dodavanja = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),
                }).Single();


            return View(obj);
        }
  
        public IActionResult PrikazObavijesti(int pageNumber = 1, int pageSize = 4)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var model = dbContext.Obavijests/*/*.Where(x => pretraga == null || (x.Naslov.ToLower().StartsWith(pretraga.ToLower())))*/
            .Select(o => new PrikazObavijesti.Row
            {
                obavijestID = o.ID,
                Naslov = o.Naslov,
                Text = o.Text,
                DatumObj = o.DatumVrijeme,
                ImeRecepcionera = o.Osoblje.Ime + " " + o.Osoblje.Prezime
            }).OrderByDescending(o => o.DatumObj).Skip(ExcludeRecords).Take(pageSize).AsNoTracking().ToList();

            int brojac = dbContext.Zahtjevs.Count();
            var result = new PagedResult<PrikazObavijesti.Row>
            {
                Data = model,
                TotalItems = brojac,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            return View(result);
        }

        //public IActionResult PrikazObavijesti()
        //{
        //    PrikazObavijesti all = new PrikazObavijesti();
        //    all.obavijesti = dbContext.Obavijests/*.Where(x => pretraga == null || (x.Naslov.ToLower().StartsWith(pretraga.ToLower())))*/
        //    .Select(o => new PrikazObavijesti.Row
        //    {
        //        obavijestID = o.ID,
        //        Naslov = o.Naslov,
        //        Text = o.Text,
        //        DatumObj = o.DatumVrijeme,
        //        ImeRecepcionera = o.Osoblje.Ime + " " + o.Osoblje.Prezime

        //    }).ToList();

        //    all.obavijesti = all.obavijesti.OrderByDescending(x => x.DatumObj).ToList();

        //    //TempData["all"] = all.obavijesti;
        //    return View(all);
        //}


        public async Task<IActionResult> SnimiAsync(DodajObavijestVM obj)
        {

            var user = await _userManager.GetUserAsync(User);
            var referent = dbContext.Osobljes.Where(a => a.KorisnikID == user.Id).FirstOrDefault();
            Obavijest ob;
            if (obj.obavijestID == 0)
            {
                ob = new Obavijest();
                ob.DatumVrijeme = DateTime.Now.ToString("23.1.2021. 01:02:45");

                dbContext.Add(ob);
            }
            else
            {
                ob = dbContext.Obavijests.Find(obj.obavijestID);
            }


            ob.Naslov = obj.Naslov;
            ob.Text = obj.Text;

            ob.OsobljeID = referent.Korisnik.Osoblje.ID;

            dbContext.SaveChanges();

            return Redirect("/Recepcija/PrikazObavijesti");
        }

        public IActionResult Obrisi(int obavijestID)
        {
            var obrisana = dbContext.Obavijests.Find(obavijestID);

            dbContext.Remove(obrisana);
            dbContext.SaveChanges();
            return Redirect("/Recepcija/PrikazObavijesti");
        }

        public IActionResult PregledObavijesti(int obavijestID)
        {
            var ob = dbContext.Obavijests.Include(a => a.Osoblje).Where(a => a.ID == obavijestID).FirstOrDefault();

            PregledObavijesti po = new PregledObavijesti();
            po.obavijestID = ob.ID;
            po.Naslov = ob.Naslov;
            po.Text = ob.Text;
            po.ImeRecepcionera = ob.Osoblje.Ime + " " + ob.Osoblje.Prezime;
            po.DatumObj = ob.DatumVrijeme;

            return View(po);
        }

        //public PartialViewResult PretragaObavijesti(string pretraga)
        //{
        //    PrikazObavijesti all = new PrikazObavijesti();
        //    all.obavijesti = dbContext.Obavijests.Where(x => pretraga == null || (x.Naslov.ToLower().StartsWith(pretraga.ToLower())))
        //    .Select(o => new PrikazObavijesti.Row
        //    {
        //        obavijestID = o.ID,
        //        Naslov = o.Naslov,
        //        Text = o.Text,
        //        DatumObj = o.DatumVrijeme,
        //        ImeRecepcionera = o.Osoblje.Ime + " " + o.Osoblje.Prezime

        //    }).ToList();

        //    all.obavijesti = all.obavijesti.OrderByDescending(x => x.DatumObj).ToList();

        //    all.pretraga = pretraga;

        //    return PartialView(all);
        //}

        public IActionResult FilterSoba()
        {
            return View();
        }
        public IActionResult PrikazSoba(string Soba, string Krevet)
        {
            var db = dbContext.Sobas.Where(a => (Soba == null || a.BrojSobe.StartsWith(Soba)) && (Krevet == "0" || a.BrojKreveta.ToString() == Krevet)).
                Select(a => new PregledSobaVM.Row
                {
                    ID = a.ID,
                    BrojSobe = a.BrojSobe,
                    BrojKreveta = a.BrojKreveta.ToString(),
                }).ToList();
            foreach (var item in db)
            {
                var lista = dbContext.Ugovors.Where(x => x.SobaID == item.ID && string.IsNullOrWhiteSpace(x.DatumIseljenja)).Include(a => a.Student).ToList();
                if (lista.Count == 0)
                {
                    item.Studenti = "Slobodna soba";
                }
                else
                {

                    foreach (var item2 in lista)
                    {
                        item.Studenti += item2.Student.Ime + " " + item2.Student.Prezime + "; ";
                    }
                    if (lista.Count.ToString() == item.BrojKreveta)
                    {
                        item.Popunjena = true;
                    }
                }

            }

            PregledSobaVM model = new PregledSobaVM();
            model.Sobe = db;
            return View(model);
        }
        public IActionResult DetaljiPrikazSoba(int SobaID)
        {
            var model = dbContext.Sobas.Where(a => a.ID == SobaID).Select(a => new DetaljiPrikazSobavm
            {
                ID = a.ID,
                Broj_Sobe = a.BrojSobe,
                Sprat = a.Sprat,
                Napomena = a.Napomena,
                ImaBalkon = a.ImaBalkon ? "DA" : "NE",
                bROJ_Kreveta = a.BrojKreveta.ToString()


            }).FirstOrDefault();
            var studenti = dbContext.Ugovors.Where(a => a.SobaID == SobaID && string.IsNullOrWhiteSpace(a.DatumIseljenja)).Select(a => new DetaljiPrikazSobavm.Studenti
            {
                ID = a.ID,
                Ime = a.Student.Ime + " " + a.Student.Prezime
            }).ToList();
            model.studenti = studenti;
            model.Popunjena = model.bROJ_Kreveta == studenti.Count.ToString();
            return View(model);
        }
        public IActionResult DodajUsobu(int SobaID, int StudentID, int KarticaID)
        {
            var Slobodnesobe = new List<SelectListItem>();

            if (SobaID == 0)
            {
                var Sobe = dbContext.Sobas.ToList();
                foreach (var item in Sobe)
                {
                    var brojac = dbContext.Ugovors.Where(a => a.SobaID == item.ID && string.IsNullOrWhiteSpace(a.DatumIseljenja)).Count();

                    if (item.BrojKreveta > brojac)
                    {
                        Slobodnesobe.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.BrojSobe });
                    }

                }
            }
            else
            {
                Slobodnesobe = dbContext.Sobas.Where(a => a.ID == SobaID).Select(item => new SelectListItem
                {
                    Value = item.ID.ToString(),
                    Text = item.BrojSobe
                }).ToList();
                   
            }


            var model = new DodajUSobuVM();
            var vecBioUseljen = dbContext.Ugovors.Where(a => a.StudentID == StudentID).FirstOrDefault();

          
            if (StudentID==0)
            {
                var Primljenistudenti = dbContext.Ugovors.Select(a => a.StudentID).ToList();
                var studenti = dbContext.Students.Where(a => !Primljenistudenti.Contains(a.ID) || !a.Uselio).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Ime + " " + a.Prezime
                }).ToList();
            model.Studenti = studenti;
            }
            else
            {
                var studenti = dbContext.Students.Where(a => a.ID==StudentID).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.Ime + " " + a.Prezime
                }).ToList();
              
                model.Studenti = studenti;
                
            }
            if (vecBioUseljen == null)
            {
                var ZauzeteKartice = dbContext.Ugovors.Select(a => a.KarticaID).ToList();
                var kartice = dbContext.Karticas.Where(a => !ZauzeteKartice.Contains(a.ID)).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.BrojKartice
                }).ToList();

                model.BrojKartice = kartice;
               
            }
            else
            {
               var kartice = dbContext.Karticas.Where(a => a.ID == vecBioUseljen.KarticaID).Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.BrojKartice
                }).ToList();
                model.BrojKartice = kartice;

            }


            model.StudentID = StudentID;
            model.Soba = Slobodnesobe;
            model.SobaID = SobaID;
            return View(model);
        }
        public async Task<IActionResult> SnimiUgovor(DodajUSobuVM admir)
        {
            var user = await _userManager.GetUserAsync(User);
            var osoblje = dbContext.Osobljes.Where(a => a.KorisnikID == user.Id).FirstOrDefault();

            var ugovor = new Ugovor
            {
                SobaID = admir.SobaID,
                StudentID = admir.StudentID,
                KarticaID = admir.BrojKarticeID,
                DodanUgovorOsobljeID = osoblje.ID,
                DatumUseljenja= DateTime.Now.ToString("dd/MM/yyyy")
        };
            dbContext.Add(ugovor);
            var Primljeni = dbContext.Students.Find(admir.StudentID);
            Primljeni.Uselio = true;
            if (Primljeni.KorisnikID == null)
            {
                var korisnik = new Korisnik();
                korisnik.Email = Primljeni.Email;
                korisnik.UserName = Primljeni.Email;
                korisnik.EmailConfirmed = true;
                //korisnik.PhoneNumber = Primljeni;

                IdentityResult result = _userManager.CreateAsync(korisnik, admir.Password).Result;
                if (!result.Succeeded)
                {
                    return Content("errors: " + string.Join('|', result.Errors));
                }
                Primljeni.Korisnik = korisnik;
                PosaljiMail(Primljeni.Email, admir.Password);
            }
            dbContext.SaveChanges();
            return Redirect("/Recepcija/DetaljiPrikazSoba?SobaID=" + admir.SobaID);
        }
        public IActionResult FilterStudenata()
        {
            return View();
        }
        public IActionResult PrikazStudenata(string pretraga, string Tip)
        {
            var tip = Tip == "0" ? true : false;
            var prijave = dbContext.Students.Where(a => (pretraga == null || (a.Ime + ' ' + a.Prezime).ToLower().StartsWith(pretraga.ToLower())
           || (a.Prezime + ' ' + a.Ime).ToLower().StartsWith(pretraga.ToLower())) && a.Uselio == tip).Select(b => new PrikazStudenataVM.Row
           {
               ID = b.ID,
               Ime = b.Ime,
               Prezime = b.Prezime,
               Uselio = b.Uselio,
               Soba = dbContext.Ugovors.Where(c => c.StudentID == b.ID && c.DatumIseljenja==null).Select(a => a.Soba.BrojSobe).FirstOrDefault(),
               BrojKartice = dbContext.Ugovors.Where(c => c.StudentID == b.ID).Select(a => a.Kartica.BrojKartice).FirstOrDefault(),

           }).ToList();
            var model = new PrikazStudenataVM();
            model.Studenti = prijave;

            return View(model);
        }
        public IActionResult DetaljiPrikazStudenata(int StudentID)
        {
            var model = dbContext.Students.Where(a => StudentID == a.ID).Select(admir => new DetaljiPrikazStudenataVM
            {
                ID = admir.ID,
                Ime = admir.Ime,
                Prezime = admir.Prezime,
                ImeOca = admir.ImeOca,
                MjestoRodjenjaID = admir.MjestoRodjenjaID,
                MjestoRodjenja = admir.MjestoRodjenja.Naziv,
                ZanimanjeRoditelja = admir.ZanimanjeRoditelja,
                PolID = admir.PolID,
                Pol = admir.Pol.Naziv,
                JMBG = admir.JMBG,
                LicnaKarta = admir.LicnaKarta,
                DatumRodjenja = admir.DatumRodjenja.ToString(),
                Mobitel = admir.Mobitel,
                Email = admir.Email,

                Adresa = admir.Lokacija.Adresa,
                MjestoStanovanjaID = admir.Lokacija.MjestoStanovanjaID,
                MjestoStanovanja = admir.Lokacija.MjestoStanovanja.Naziv,
                KantonID = admir.Lokacija.KantonID,
                Kanton = admir.Lokacija.Kanton.Naziv,

                FakultetID = admir.FakultetID,
                Fakultet = admir.Fakultet.Naziv,
                TipKandidataID = admir.TipKandidataID,
                TipKandidata = admir.TipKandidata.Naziv,
                BrojIndeksa = admir.BrojIndeksa,
                CiklusStudijaID = admir.CiklusStudijaID,
                CiklusStudija = admir.CiklusStudija.Naziv,
                GodinaStudijaID = admir.GodinaStudijaID,
                GodinaStudija = admir.GodinaStudija.Naziv,
                Uselio = admir.Uselio


            }).Single();
            return View(model);
        }
        public IActionResult IseliStudenta(int StudentID)
        {
            var model = dbContext.Ugovors.Where(a => StudentID == a.StudentID && a.DatumIseljenja==null).FirstOrDefault();
            model.DatumIseljenja = DateTime.Now.ToString("dd/MM/yyyyy");
            var student = dbContext.Students.Find(StudentID);
            student.Uselio = false;
            dbContext.SaveChanges();
            return Redirect(url: "/Recepcija/DetaljiPrikazStudenata?StudentID=" + StudentID);
        }

        public  IActionResult PrikazZahtjeva(int pageNumber = 1, int pageSize = 8)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var model = dbContext.Zahtjevs.Select(a => new PrikazZahtjevaVM.Row
            {
                ID = a.ID,
                ImePrezime = a.Ugovor.Student.Ime + " " + a.Ugovor.Student.Prezime,
                Zahtjev = a.VrstaZahtjeva.Naziv,
                Datum = a.Datum,
                Soba = a.Ugovor.Soba.BrojSobe
            }).OrderByDescending(a=>a.Datum).Skip(ExcludeRecords).Take(pageSize);
            int brojac = dbContext.Zahtjevs.Count();
            //var Model = new PrikazZahtjevaVM();
            var result = new PagedResult<PrikazZahtjevaVM.Row>
            {
                Data = model.AsNoTracking().ToList(),
                TotalItems = brojac,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }
        public async void PosaljiMail(string mail,string password)
        {
            await _emailService.SendEmailAsync(mail, "Studentski hotel Mostar", "<h1>Vasi pristupni podaci su sledeci </h1>" +
                     $"<p>E-mail : {mail}</p>" +
                    $"<p>Sifra : {password}</p>");
               
        }
    }
}
