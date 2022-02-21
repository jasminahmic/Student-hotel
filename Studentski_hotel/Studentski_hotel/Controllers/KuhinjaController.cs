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
                .ToLower().StartsWith(pretraga.ToLower())) || (a.BrojKartice).ToLower().StartsWith(pretraga.ToLower()))
            .Select(u => new PrikazKarticaVM.Row
            {
                karticaID = u.ID,
                BrojKartice = u.BrojKartice,
                StanjeKartice = (int) u.StanjeNaKartici
            }).ToList();

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

            Kartica card;
            if (kartica.karticaID == 0)
            {
                card = new Kartica();

                dbContext.Add(card);
            }
            else
            {
                card = dbContext.Karticas.Find(kartica.karticaID);
            }


            if (dbContext.Karticas.Any(x => x.BrojKartice == kartica.BrojKartice))
            {
                ModelState.AddModelError(nameof(card.BrojKartice), "Kartica sa tim brojem vec postoji");


            } else
            {
                card.BrojKartice = kartica.BrojKartice;
                card.StanjeNaKartici = kartica.StanjeKartice;
                dbContext.SaveChanges();
            }
                return Redirect("/Kuhinja/FilterKartica");
        }
    }
}
