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
            List<SelectListItem> CiklusStudija = dbContext.CiklusStudijas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> TipKandidata = dbContext.tipKandidatas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> Fakultet = dbContext.Fakultets.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();

            List<SelectListItem> GodinaStudija = dbContext.GodinaStudijas.Select(a => new SelectListItem
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

            SnimiVM snimiVM = new SnimiVM();
            //snimiVM.MjestoStanovanja = opstine;
            snimiVM.MjestoRodjenja = opstine;
            snimiVM.CiklusStudija = CiklusStudija;
            snimiVM.TipKandidata = TipKandidata;
            snimiVM.Fakultet = Fakultet;
            snimiVM.GodinaStudija = GodinaStudija;
            snimiVM.Kanton = kanton;
            snimiVM.Pol = pol;

            return View(snimiVM);
        }
        public IActionResult Snimi(SnimiVM admir)
        {
            Konkurs konkurs = new Konkurs();

            konkurs.Ime = admir.Ime;
            konkurs.Prezime = admir.Prezime;
            konkurs.ImeOca = admir.ImeOca;
            konkurs.MjestoRodjenjaID = admir.MjestoRodjenjaID;
            konkurs.ZanimanjeRoditelja = admir.ZanimanjeRoditelja;
            konkurs.PolID = admir.PolID;
            konkurs.JMBG = admir.JMBG;
            konkurs.LicnaKarta = admir.LicnaKarta;
            konkurs.DatumRodjenja = admir.DatumRodjenja.ToString();
            konkurs.Mobitel = admir.Mobitel;
            konkurs.Email = admir.Email;

            konkurs.Adresa = admir.Adresa;
            konkurs.MjestoStanovanjaID = admir.MjestoStanovanjaID;
            konkurs.KantonID = admir.KantonID;

            konkurs.FakultetID = admir.FakultetID;
            konkurs.TipKandidataID = admir.TipKandidataID;
            konkurs.BrojIndeksa = admir.BrojIndeksa;
            konkurs.CiklusStudijaID = admir.CiklusStudijaID;
            konkurs.GodinaStudijaID = admir.GodinaStudijaID;
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
