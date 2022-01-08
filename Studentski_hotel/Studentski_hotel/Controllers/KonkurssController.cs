using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBdata.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Studentski_hotel.Data;
using Studentski_hotel.Models.Konkurss;

namespace Studentski_hotel.Controllers
{
    public class KonkurssController : Controller
    {
        private ApplicationDbContext dbContext;

        public KonkurssController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult KonkursForma()
        {
            List<SelectListItem> opstina = dbContext.Grads.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> ciklusStudija = dbContext.CiklusStudijas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> tipKandidata = dbContext.tipKandidatas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> fakultet = dbContext.Fakultets.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> godinaStudija = dbContext.GodinaStudijas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> kanton = dbContext.Kantons.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            List<SelectListItem> pol = dbContext.Pols.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            KonkursPodaciVM konkurs = new KonkursPodaciVM();
            konkurs.MjestoStanovanja = opstina;
            konkurs.MjestoRodjenja = opstina;
            konkurs.CiklusStudija = ciklusStudija;
            konkurs.TipKandidata = tipKandidata;
            konkurs.Fakultet = fakultet;
            konkurs.GodinaStudija = godinaStudija;
            konkurs.Kanton = kanton;
            konkurs.Pol = pol;

            return View(konkurs);
        }
        public IActionResult SnimiZahtjev(KonkursPodaciVM noviStudent)
        {
            Konkurs konkurs = new Konkurs();

            konkurs.Ime = noviStudent.Ime;
            konkurs.Prezime = noviStudent.Prezime;
            konkurs.ImeOca = noviStudent.ImeOca;
            konkurs.MjestoRodjenjaID = noviStudent.MjestoRodjenjaID;
            konkurs.ZanimanjeRoditelja = noviStudent.ZanimanjeRoditelja;
            konkurs.PolID = noviStudent.PolID;
            konkurs.JMBG = noviStudent.JMBG;
            konkurs.LicnaKarta = noviStudent.LicnaKarta;
            konkurs.DatumRodjenja = noviStudent.DatumRodjenja.ToString();
            konkurs.Mobitel = noviStudent.Mobitel;
            konkurs.Email = noviStudent.Email;

            konkurs.Adresa = noviStudent.Adresa;
            konkurs.MjestoStanovanjaID = noviStudent.MjestoStanovanjaID;
            konkurs.KantonID = noviStudent.KantonID;

            konkurs.FakultetID = noviStudent.FakultetID;
            konkurs.TipKandidataID = noviStudent.TipKandidataID;
            konkurs.BrojIndeksa = noviStudent.BrojIndeksa;
            konkurs.CiklusStudijaID = noviStudent.CiklusStudijaID;
            konkurs.GodinaStudijaID = noviStudent.GodinaStudijaID;
            dbContext.Add(konkurs);
            dbContext.SaveChanges();
            return Redirect(url: "/");
        }
        public JsonResult PrikazOpstina(int KantonID)
        {
            List<SelectListItem> opstine = dbContext.Grads.Where(a => a.KantonID == KantonID).Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return Json(opstine);
        }
    }
}
