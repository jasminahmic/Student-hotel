﻿using System;
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
using Studentski_hotel.notHub;
using Microsoft.AspNetCore.SignalR;
using Studentski_hotel.Models.Kuhinja;

namespace Studentski_hotel.Controllers
{
    [Autorizacija(false, true, false, false)]
    public class RecepcijaController : Controller
    {
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private ApplicationDbContext dbContext;
        private IEmailService _emailService;
        IHubContext<NotHub> _hubContext;

        public RecepcijaController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ApplicationDbContext _dbContext, IEmailService emailService, IHubContext<NotHub> hubContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbContext = _dbContext;
            _emailService = emailService;
            _hubContext = hubContext;

        }

        public IActionResult EditObavijest(int obavijestID)
        {
            DodajObavijestVM notification = obavijestID == 0 ? new DodajObavijestVM() :
                dbContext.Obavijests.Where(x => x.ID == obavijestID).
                Select(o => new DodajObavijestVM
                {
                    obavijestID = o.ID,
                    Naslov = o.Naslov,
                    Text = o.Text.Trim(),
                    RecepcionerID = o.OsobljeID,
                    datum_dodavanja = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"),
                }).Single();


            return View(notification);
        }
        public IActionResult PrikazObavijesti(string pretraga, int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            PrikazObavijestiVM notificationsList = new PrikazObavijestiVM();
            notificationsList.obavijesti = dbContext.Obavijests.Where(x => pretraga == null || (x.Naslov.ToLower().StartsWith(pretraga.ToLower())))
            .Select(o => new PrikazObavijestiVM.Row
            {
                obavijestID = o.ID,
                Naslov = o.Naslov,
                Text = o.Text,
                DatumObj = o.DatumVrijeme,
                ImeRecepcionera = o.Osoblje.Ime + " " + o.Osoblje.Prezime
            }).OrderByDescending(o => o.DatumObj).Skip(ExcludeRecords).Take(pageSize).AsNoTracking().ToList();

            int brojac = dbContext.Obavijests.Count();
  
            var result = new PagedResult<PrikazObavijestiVM.Row>
            {
                Data = notificationsList.obavijesti,
                PageNumber= pageNumber,
                TotalItems = brojac,
                PageSize = pageSize,
            };
            
            return View(result);
        }

        public async Task<IActionResult> SnimiObavijest(DodajObavijestVM notification)
        {

            var user = await _userManager.GetUserAsync(User);
            var referent = dbContext.Osobljes.Where(a => a.KorisnikID == user.Id).FirstOrDefault();
            Obavijest obavijest;
            if (notification.obavijestID == 0)
            {
                obavijest = new Obavijest();
                obavijest.DatumVrijeme = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");

                dbContext.Add(obavijest);
            }
            else
            {
                obavijest = dbContext.Obavijests.Find(notification.obavijestID);
            }

            obavijest.Naslov = notification.Naslov;
            obavijest.Text = notification.Text;
            obavijest.OsobljeID = referent.Korisnik.Osoblje.ID;

            dbContext.SaveChanges();
            await _hubContext.Clients.All.SendAsync("SlanjeObavijesti",
                                                                    notification.Naslov, 
                                                                    notification.Text,
                                                                    user.Osoblje.ID, 
                                                                    obavijest.DatumVrijeme, 
                                                                    referent.Ime + "" + referent.Prezime,
                                                                    notification.obavijestID
                                                                );
            return Redirect("/Recepcija/PrikazObavijesti");
        }

        public IActionResult Obrisi(int obavijestID)
        {
            var notification = dbContext.Obavijests.Find(obavijestID);

            dbContext.Remove(notification);
            dbContext.SaveChanges();
            return Redirect("/Recepcija/PrikazObavijesti");
        }

        public IActionResult PregledObavijesti(int obavijestID)
        {
            var obavijest = dbContext.Obavijests.Include(a => a.Osoblje).Where(a => a.ID == obavijestID).FirstOrDefault();

            PregledObavijesti selectedNotification = new PregledObavijesti();
            selectedNotification.obavijestID = obavijest.ID;
            selectedNotification.Naslov = obavijest.Naslov;
            selectedNotification.Text = obavijest.Text;
            selectedNotification.ImeRecepcionera = obavijest.Osoblje.Ime + " " + obavijest.Osoblje.Prezime;
            selectedNotification.DatumObj = obavijest.DatumVrijeme;

            return View(selectedNotification);
        }

        public IActionResult FilterSoba()
        {
            return View();
        }
        public IActionResult PrikazSoba(string Soba, string Krevet)
        {
            var sobe = dbContext.Sobas.Where(a => (Soba == null || a.BrojSobe.StartsWith(Soba)) && (Krevet == "0" || a.BrojKreveta.ToString() == Krevet)).
                Select(a => new PregledSobaVM.Row
                {
                    ID = a.ID,
                    BrojSobe = a.BrojSobe,
                    BrojKreveta = a.BrojKreveta.ToString(),
                }).ToList();
            foreach (var item in sobe)
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
            model.Sobe = sobe;
            return View(model);
        }
        public IActionResult DetaljiPrikazSoba(int SobaID)
        {
            var soba = dbContext.Sobas.Where(a => a.ID == SobaID).Select(a => new DetaljiPrikazSobavm
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
            soba.studenti = studenti;
            soba.Popunjena = soba.bROJ_Kreveta == studenti.Count.ToString();
            return View(soba);
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
            var tip = Tip == "0";
            var prijave = dbContext.Students.Where(a => (pretraga == null || (a.Ime + ' ' + a.Prezime).ToLower().StartsWith(pretraga.ToLower())
           || (a.Prezime + ' ' + a.Ime).ToLower().StartsWith(pretraga.ToLower())) && a.Uselio == tip).Select(b => new PrikazStudenataVM.Row
           {
               ID = b.ID,
               Ime = b.Ime,
               Prezime = b.Prezime,
               Uselio = b.Uselio,
               Soba = dbContext.Ugovors.Where(c => c.StudentID == b.ID && c.DatumIseljenja == null).Select(a => a.Soba.BrojSobe).FirstOrDefault(),
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

        public IActionResult FilterUplata()
        {
            return View();
        }
        public IActionResult PrikazUplata(string pretraga)
        {
            PrikazUplataVm paymentList = new PrikazUplataVm();
            paymentList.uplate = dbContext.Uplatas
                .Where(a => (pretraga == null || (a.Ugovor.Student.Ime + ' ' + a.Ugovor.Student.Prezime)
                .ToLower().StartsWith(pretraga.ToLower())) || (a.Ugovor.Student.Ime + ' ' + a.Ugovor.Student.Ime).ToLower().StartsWith(pretraga.ToLower()))
                .Include(x => x.NacinUplate)
                .Include(x => x.Osoblje)
                .Include(x => x.Ugovor)
            .Select(u => new PrikazUplataVm.Row
            {
               uplataID = u.ID,
               Student = u.Ugovor.Student.Ime + " " + u.Ugovor.Student.Prezime,
               ImeRecepcionera = u.NacinUplate.Naziv == "Na recepciji" ? u.Osoblje.Ime + " " + u.Osoblje.Prezime : " ",
               DatumUplate = u.Datum,
               NacinUplate = u.NacinUplate.Naziv,
               VrijednostUplate = u.Stanje.ToString() + "KM"

            }).OrderByDescending(u => u.DatumUplate).ToList();

            return View(paymentList);
        }

        public IActionResult DetaljiUplate(int uplataID)
        {
            var uplata = dbContext.Uplatas.Include(x => x.NacinUplate)
                                            .Include(x => x.Osoblje)
                                            .Include(x => x.Ugovor)
                .Where(u => u.ID == uplataID).FirstOrDefault();

            var student = dbContext.Students.Where(s => s.ID == uplata.Ugovor.StudentID).FirstOrDefault();
           
            DetaljiUplateVM selected = new DetaljiUplateVM();
            selected.uplataID = uplata.ID;
            selected.StudentID = uplata.UgovorID;
            selected.StudentIme = student.Ime + " " + student.Prezime;

            selected.ImeRecepcionera = uplata.NacinUplate.Naziv == "Na recepciji" ? uplata.Osoblje.Ime + " " + uplata.Osoblje.Prezime : " ";
            selected.DatumUplate = uplata.Datum;
            selected.NacinUplate = uplata.NacinUplate.Naziv;
            selected.VrijednostUplate = uplata.Stanje.ToString() + "KM";
            selected.RazlogUplate = uplata.Stanje == 206 ? "Cijeli iznos" : "Ishrana";

            return View(selected);
        }

        public IActionResult EditUplate(int uplataID, int NacinUplateID) {

            List<SelectListItem> studenti = dbContext.Ugovors.Where(a => a.DatumIseljenja == null)
                .Include(x => x.Student).
                Where(s=> s.Student.Uselio)
              .Select(a => new SelectListItem
            {
                Value = a.ID.ToString(),
                Text = a.Student.Ime + " " + a.Student.Prezime
            }).ToList();

            List<SelectListItem> naciniUplate = dbContext.NacinUplates.Select(a => new SelectListItem
            {
                Value = a.ID.ToString(),
                Text = a.Naziv
            }).ToList();

            DodajUplatuVM novaUplata = uplataID == 0 ? new DodajUplatuVM() :
                dbContext.Uplatas.Where(x => x.ID == uplataID).
                Select(o => new DodajUplatuVM
                {
                    uplataID = uplataID,
                    VrijednostUplate = (int) o.Stanje,
                    RecepcijaID = o.Osoblje.ID,
                    DatumUplate = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"),
                }).Single();

            novaUplata.Studenti = studenti;
            novaUplata.NaciniUplate = naciniUplate;

            return View(novaUplata);
        }

        public async Task<IActionResult> SnimiUplatu(DodajUplatuVM novaUplata)
        {

            var user = await _userManager.GetUserAsync(User);
            var referent = dbContext.Osobljes.Where(a => a.KorisnikID == user.Id).FirstOrDefault();
            Uplata uplata;
            if (novaUplata.uplataID == 0)
            {
                uplata = new Uplata();
                uplata.Datum = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");

                dbContext.Add(uplata);
            }
            else
            {
                uplata = dbContext.Uplatas.Find(novaUplata.uplataID);
            }

            uplata.NacinUplateID = novaUplata.NacinUplateID;
            uplata.Stanje = (float) novaUplata.VrijednostUplate;
            uplata.OsobljeID = referent.Korisnik.Osoblje.ID;
            uplata.UgovorID = novaUplata.StudentID;

            dbContext.SaveChanges();

            return Redirect("/Recepcija/FilterUplata");
        }

        public IActionResult ObrisiUplatu(int uplataID)
        {
            var uplata = dbContext.Uplatas.Find(uplataID);

            dbContext.Remove(uplata);
            dbContext.SaveChanges();
            return Redirect("/Recepcija/FilterUplata");
        }
        public IActionResult UplateStudenta(int studentID, int pageNumber=1, int pageSize = 2)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            PregledUplataVM uplateStudenta = new PregledUplataVM();
            uplateStudenta.uplateStudenta = dbContext.Uplatas
                .Include(x => x.NacinUplate)
                .Include(x => x.Osoblje)
                .Include(x => x.Ugovor)
                .Where(x => x.Ugovor.StudentID == studentID)
            .Select(u => new PregledUplataVM.Row
            {
                uplataID = u.ID,
                ImeRecepcionera = u.Osoblje.Ime + " " + u.Osoblje.Prezime,
                NacinUplate = u.NacinUplate.Naziv,
                DatumUplate = u.Datum,
                Iznos = u.Stanje.ToString() + "KM"
            }).OrderBy(u => u.DatumUplate).Skip(ExcludeRecords).Take(pageSize).AsNoTracking().ToList();


            int brojac = dbContext.Uplatas.Where(x => x.Ugovor.StudentID == studentID).Count();

            var result = new PagedResult<PregledUplataVM.Row>
            {
                Data = uplateStudenta.uplateStudenta,
                PageNumber = pageNumber,
                TotalItems = brojac,
                PageSize = pageSize,
            };


            return View(result);
        }

        public IActionResult ListaZaNapomenuti()
        {
            DateTime currently = DateTime.Now;

            DateTime deadline = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 11);

            var uplataDate = new DateTime();


            foreach (var item in dbContext.Uplatas)
            {
                //ovo je datum zadnje uplate
                uplataDate = Convert.ToDateTime(item.Datum.Substring(0));
            }

            var uplatePoStudentu = dbContext.Uplatas.Where(x=> uplataDate < deadline).GroupBy(u => u.Ugovor.StudentID)
                .Select(e => new { e.Key, Count = e.Count() })
                .ToDictionary(e => e.Key, e => e.Count);

            
            FilterStudenataNeplacenoVM lista = new FilterStudenataNeplacenoVM();
          
            foreach (var item in uplatePoStudentu) {

                if (uplataDate.Month != deadline.Month)
                {

                    lista.studentiNisuUplatili = dbContext.Students.Where(x => x.ID == item.Key).Select(s => new FilterStudenataNeplacenoVM.Row
                    {
                        studentID = s.ID,
                        EmailStudenta = s.Email,
                        Mjesec = uplataDate.Month.ToString()

                    }).ToList();
                }
            }


            lista.studentiNisuUplatili = dbContext.Ugovors.Where(x => dbContext.Uplatas.All(u=> u.UgovorID !=x.ID))
                                           .Select(s => new FilterStudenataNeplacenoVM.Row
                                           {
                                               studentID = s.StudentID,
                                               EmailStudenta = s.Student.Email,
                                               Mjesec = "October"
                                           }).ToList();

            lista.CurrentDate = currently.ToString("dd MMMM yyyy");


            return View(lista);
        }

        public async Task<IActionResult> PosaljiUpozorenje(string email, string sadrzajEmaila)
        {
            var user = await _userManager.GetUserAsync(User);

            await _emailService.SendEmailAsync(email, "Studentski hotel Mostar", "<h1>Upozorenje za placanje smjestaja i ishrane </h1>" +
                     $"<p>E-mail : {user.Email}</p>" +
                    $"<p>Razlog : {sadrzajEmaila}</p>");

            return Redirect("/Recepcija/ListaZaNapomenuti");
        }

        public IActionResult PrikazStudenataKojiNisuTU()
        {
            var sviStudenti = dbContext.Ugovors.Include(x => x.Student).Select(p => new PrikazPrisutniStudenataVM.Row
            {
                ID = p.ID,
                Ime = p.Student.Ime,
                Prezime = p.Student.Prezime,
                Uselio = p.Student.Uselio,
                Soba = dbContext.Ugovors.Where(c => c.StudentID == p.ID && c.DatumIseljenja == null).Select(a => a.Soba.BrojSobe).FirstOrDefault(),
                BrojKartice = dbContext.Ugovors.Where(c => c.StudentID == p.ID).Select(a => a.Kartica.BrojKartice).FirstOrDefault(),
            }).ToList();


            var model = new PrikazPrisutniStudenataVM();
            foreach (var item in sviStudenti)
            {
                if (dbContext.NajavaOdlaskas.Any(x => x.UgovorID == item.ID && x.DatumPovratka != null))
                {
                    item.Prisutan = false;
                }
                else
                {
                    item.Prisutan = true;
                }
            }

            var nisuPrisutni = sviStudenti.Where(x => !x.Prisutan).ToList();

            model.Studenti = nisuPrisutni;

            return View(model);
        }

        public IActionResult OznaciPrisutnim(string ime)
        {
            var student = dbContext.Students.Where(x => x.Ime == ime).FirstOrDefault();
            var ugovor = dbContext.Ugovors.Where(x => x.StudentID == student.ID).FirstOrDefault();
            var objectForDeleting = dbContext.NajavaOdlaskas.Where(x => x.UgovorID == ugovor.ID)
                .OrderByDescending(a => a.ID).FirstOrDefault();

            if (objectForDeleting != null)
            {
                dbContext.Remove(objectForDeleting);
                dbContext.SaveChanges();
            }

            return Redirect("/Recepcija/PrikazStudenataKojiNisuTU");
        }

        public IActionResult PregledBlackListe()
        {
            
            BlackListItemsVM lista = new BlackListItemsVM();
            lista.studenti = dbContext.Students.Where(x => x.BlackListID != null).Select(s => new BlackListItemsVM.Row
            {
                ImeStudenta = s.Ime + " " + s.Prezime,
                Razlog = s.RazlogZaBlackListu,
                studentID = s.ID

            }).ToList();
          
            return View(lista);

        }

        public IActionResult EditBlackList(int studentID)
        {
            List<SelectListItem> studenti = dbContext.Students.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.Ime + " " + s.Prezime
            }).ToList();

            List<SelectListItem> bl = dbContext.BlackLists.Select(a => new SelectListItem
            {
                Value = a.ID.ToString(),
                Text = a.Naziv
            }).ToList();

            EditBlackListVM editBl = new EditBlackListVM();

                if (studentID != 0)
                {
                    editBl = dbContext.Students.Where(x => x.ID == studentID).
                    Select(o => new EditBlackListVM
                    {
                        StudentID = o.ID,
                        Razlog = o.RazlogZaBlackListu,
                        BlackListID = (int) o.BlackListID
                    }).Single();

                }

            editBl.Studenti = studenti;
            editBl.BlackList = bl;

            return View(editBl);
        }

        public IActionResult SnimiRazlog(EditBlackListVM noviStudent)
        {
            var student = dbContext.Students.Where(x => x.ID == noviStudent.StudentID).FirstOrDefault();
            student.RazlogZaBlackListu = noviStudent.Razlog;
            student.BlackListID = noviStudent.BlackListID;
            dbContext.SaveChanges();
            return Redirect("/Recepcija/PregledBlackListe");
        }

        public IActionResult SkloniStudenta(int studentID)
        {
            var student = dbContext.Students.Where(x => x.ID == studentID).FirstOrDefault();

            student.RazlogZaBlackListu = null;
            student.BlackListID = null;
            dbContext.SaveChanges();
            return Redirect("/Recepcija/PregledBlackListe");
        }


    }
}
