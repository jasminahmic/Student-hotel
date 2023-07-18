using DBdata.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Studentski_hotel.Data;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.Konkurss;
using Studentski_hotel.Models.Referent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Services
{
    public class KonkursService:IKonkursService
    {
        private ApplicationDbContext dbContext;
        public KonkursService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public List<SelectListItem> GetOpstine()
        {
            List<SelectListItem> opstine = dbContext.Grads.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return opstine;
        }

        public List<SelectListItem> GetOpstineByKanton(int KantonID)
        {
            List<SelectListItem> opstine = dbContext.Grads.Where(a => a.KantonID == KantonID).Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return opstine;
        }

        public List<SelectListItem> GetPol()
        {
            List<SelectListItem> polovi = dbContext.Pols.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return polovi;
        }

        public List<SelectListItem> GetKanton()
        {
            List<SelectListItem> kantoni = dbContext.Kantons.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return kantoni;
        }

        public List<SelectListItem> GetCiklusStudija()
        {
            List<SelectListItem> ciklusStudija = dbContext.CiklusStudijas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return ciklusStudija;
        }

        public List<SelectListItem> GetTipKandidata()
        {
            List<SelectListItem> tipKandidata = dbContext.tipKandidatas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return tipKandidata;
        }

        public List<SelectListItem> GetFakultet()
        {
            List<SelectListItem> fakultet = dbContext.Fakultets.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return fakultet;
        }
        public List<SelectListItem> GetGodinaStudija()
        {
            List<SelectListItem> godinaStudija = dbContext.GodinaStudijas.Select(a => new SelectListItem
            {
                Text = a.Naziv,
                Value = a.ID.ToString()
            }).ToList();
            return godinaStudija;
        }

        public KonkursPodaciVM KonkursForma()
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

            return konkurs;
        }

        public void SnimiZahtjev(KonkursPodaciVM noviStudent)
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
        }

        public void DodajStudenta(DetaljiPrijavaVM admir)
        {
            Student student = new Student();
            student.Ime = admir.Ime;
            student.Prezime = admir.Prezime;
            student.ImeOca = admir.ImeOca;
            student.MjestoRodjenjaID = admir.MjestoRodjenjaID;
            student.ZanimanjeRoditelja = admir.ZanimanjeRoditelja;
            student.JMBG = admir.JMBG;
            student.PolID = admir.PolID;
            student.LicnaKarta = admir.LicnaKarta;
            student.DatumRodjenja = admir.DatumRodjenja.ToString();
            student.Mobitel = admir.Mobitel;
            student.Email = admir.Email;
            student.FakultetID = admir.FakultetID;
            student.TipKandidataID = admir.TipKandidataID;
            student.BrojIndeksa = admir.BrojIndeksa;
            student.CiklusStudijaID = admir.CiklusStudijaID;
            student.GodinaStudijaID = admir.GodinaStudijaID;
            student.Uselio = false;
            Lokacija lokacija = new Lokacija();
            lokacija.Adresa = admir.Adresa;
            lokacija.MjestoStanovanjaID = admir.MjestoRodjenjaID;
            lokacija.KantonID = admir.KantonID;
            dbContext.Add(lokacija);
            dbContext.SaveChanges();

            student.LokacijaID = lokacija.ID;
            dbContext.Add(student);
            dbContext.SaveChanges();
            var konkurs = dbContext.Konkurs.Find(admir.ID);
            var stanje = dbContext.RezultatKonkursas.Find(konkurs.RezultatKonkursaID);
            stanje.VrstaStanjaKonkursaID = 2;
            konkurs.StudentID = student.ID;
            dbContext.SaveChanges();
        }

    }
}
