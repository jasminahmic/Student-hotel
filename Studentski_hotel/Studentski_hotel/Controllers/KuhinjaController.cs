using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using DBdata.EntityModels;
using System.Threading.Tasks;
using Studentski_hotel.Models.Kuhinja;
using Studentski_hotel.Helper;
using Microsoft.AspNetCore.Identity;
using Studentski_hotel.Data;
using Studentski_hotel.notHub;
using Studentski_hotel.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Studentski_hotel.Controllers
{

    public class KuhinjaController : Controller
    {
        private UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private ApplicationDbContext dbContext;
        private IEmailService _emailService;
        IHubContext<NotHub> _hubContext;

        public KuhinjaController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, ApplicationDbContext _dbContext, IEmailService emailService, IHubContext<NotHub> hubContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbContext = _dbContext;
            _emailService = emailService;
            _hubContext = hubContext;

        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult FilterKartica()
        {
            return View();
        }

        public IActionResult PrikazKartica(string pretraga)
        {
            PrikazKarticaVM cardList = new PrikazKarticaVM();
            cardList.kartice = dbContext.Karticas
                .Where(a => (pretraga == null || (a.BrojKartice)
                .ToLower().StartsWith(pretraga.ToLower())) || 
                a.BrojKartice.ToLower().StartsWith(pretraga.ToLower())
                )
            .Select(u => new PrikazKarticaVM.Row
            {
                karticaID = u.ID,
                BrojKartice = u.BrojKartice,
                StanjeKartice = (int) u.StanjeNaKartici
            }).ToList();

            DateTime current = DateTime.Now;
            if (current.Day == 1)
            {
                FaktorisanjeObroka();
            }

            return View(cardList);
        }

        public IActionResult EditKartice(int karticaID)
        {
            DodajKarticuVM card = karticaID == 0 ? new DodajKarticuVM() :
                dbContext.Karticas.Where(x => x.ID == karticaID).
                Select(o => new DodajKarticuVM
                {
                    karticaID = o.ID,
                    BrojKartice = o.BrojKartice,
                    StanjeKartice = (int) o.StanjeNaKartici,
                    datum_dodavanja = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"),
                }).Single();


            return View(card);
        }

        [ValidateAntiForgeryToken]

        public IActionResult SnimiKarticu(DodajKarticuVM kartica)
        {

            bool cardNumberNotExist = dbContext.Karticas.All(x => x.BrojKartice != kartica.BrojKartice);

            Kartica card;
            if (kartica.karticaID == 0 && cardNumberNotExist)
            {
                card = new Kartica();

                dbContext.Add(card);
            }
            else
            {
                card = dbContext.Karticas.Find(kartica.karticaID);
            }

            if (card !=null)
            {
                card.BrojKartice = kartica.BrojKartice;
          
                card.StanjeNaKartici = kartica.StanjeKartice;
                dbContext.SaveChanges();
                return Redirect("/Kuhinja/FilterKartica");
            }

            return Redirect("/Kuhinja/EditKartice");
        }

        public IActionResult DetaljiKartice(int karticaID)
        {

            var kartica = dbContext.Karticas
                .Where(u => u.ID == karticaID).FirstOrDefault();

            var ugovor = dbContext.Ugovors
                .Include(x => x.Student)
                .ThenInclude(x => x.TipKandidata)
                .Where(s => s.KarticaID == karticaID && s.DatumIseljenja == null).FirstOrDefault();


            var trenutniMjesec = DateTime.Now.Month.ToString();
            DetaljiKarticeVM selected = new DetaljiKarticeVM();

            if (ugovor != null)
            {
                bool imaUplatu = dbContext.Uplatas.Any(u => u.UgovorID == ugovor.ID);

                if (imaUplatu)
                {
                    var zadnjaUplata = dbContext.Uplatas
                        .Where(u => u.UgovorID == ugovor.ID && u.Stanje > 20
                        ).OrderByDescending(x => x.ID).FirstOrDefault();


                    selected.karticaID = kartica.ID;
                    selected.StanjeKartice = kartica.StanjeNaKartici;
                    selected.BrojKartice = kartica.BrojKartice;
                    selected.StudentID = ugovor.StudentID;
                    selected.StudentIme = ugovor.Student.Ime + " " + ugovor.Student.Prezime;
                    selected.TipStudenta = ugovor.Student.TipKandidata.Naziv;
                    selected.RedFlag = zadnjaUplata.Datum.Substring(5, 1) != trenutniMjesec || zadnjaUplata == null ? true : false;
                }
            }

            var artikli = from a in dbContext.Artikals
                          join ac in dbContext.ArtikalCijenas on a.ID equals ac.ArtikalID
                          select new ObrokListOption
                          {
                              ArtikalID = a.ID,
                              NazivCijenaArtikla = a.NazivArtikla + " " + ac.Cijena,
                              CijenaArtikla = ac.Cijena
                          };

            var obrokLista = new List<ObrokListOption>(artikli);

           
            selected.ObrokListOptions = obrokLista;

            return View(selected);
        }

        public async Task<IActionResult> SkiniObrok(SkiniObrokVM obrok)
        {

            var user = await _userManager.GetUserAsync(User);
            var radnik = dbContext.Osobljes.Where(a => a.KorisnikID == user.Id).FirstOrDefault();

            var stringIznos = obrok.IznosObroka.ToString();
            var lastDigit = stringIznos.Substring(stringIznos.Length - 1);
            var correctIznosValue = float.Parse(stringIznos.Insert(stringIznos.Length - 1, ","));
            Obrok obrok1 = new Obrok();
            obrok1.Datum = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss");
            obrok1.Iznos = correctIznosValue;
            obrok1.OsobljeID = radnik.ID;
            obrok1.Osoblje = radnik;
            dbContext.Add(obrok1);
            dbContext.SaveChanges();

            for (int i=0; i<obrok.SelectedArtikals.Count(); i++)
            {
                dbContext.ArtikalObroks.Add(new ArtikalObrok
                {
                    ObrokID = obrok1.ID,
                    ArtikalID = obrok.SelectedArtikals[i]
                });

            }

            var kartica = dbContext.Karticas.Where(x => x.BrojKartice == obrok.brojKartice).FirstOrDefault();
            if (kartica.StanjeNaKartici >= 2)
            {
                kartica.StanjeNaKartici = kartica.StanjeNaKartici - obrok1.Iznos;
            }

            dbContext.SaveChanges();

            return Redirect("/Kuhinja/FilterKartica");
        }

        public IActionResult PrikazPrisutniStudenata()
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
                if (dbContext.NajavaOdlaskas.Any(x=> x.UgovorID == item.ID && x.DatumPovratka != null))
                {
                    item.Prisutan = false;
                } else
                {
                    item.Prisutan = true;
                }
            }

            var prisutniStudenti = sviStudenti.Where(x => x.Prisutan).ToList();

            model.Studenti = prisutniStudenti;

            return View(model);
        }

        public void FaktorisanjeObroka()
        {
            foreach (var student in dbContext.Ugovors)
            {
                if (student.DatumIseljenja == null)
                {
                    var kartica = dbContext.Karticas.Where(x => x.ID == student.KarticaID).FirstOrDefault();

                    kartica.StanjeNaKartici = 176;
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
