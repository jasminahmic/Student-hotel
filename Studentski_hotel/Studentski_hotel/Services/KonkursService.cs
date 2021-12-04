using DBdata.EntityModels;
using Studentski_hotel.Data;
using Studentski_hotel.Interface;
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
        public void DodajStudenta(DetaljiPrikazPrijavaVM admir)
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
