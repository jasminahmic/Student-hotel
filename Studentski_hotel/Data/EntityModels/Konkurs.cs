using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
   public class Konkurs
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeOca { get; set; }
        public Grad MjestoRodjenja { get; set; }
        public int MjestoRodjenjaID { get; set; }
        public string ZanimanjeRoditelja { get; set; }
        public Pol Pol { get; set; }
        public int PolID { get; set; }
        public string JMBG { get; set; }
        public string LicnaKarta { get; set; }
        public string Mjesto_izdavanja_LK { get; set; }
        public string DatumRodjenja { get; set; }
        public string Mobitel { get; set; }
        public string Email { get; set; }



        public string Adresa { get; set; }
        public Grad MjestoStanovanja { get; set; }
        public int MjestoStanovanjaID { get; set; }
        public Kanton Kanton { get; set; }
        public int KantonID { get; set; }



        public Fakultet Fakultet { get; set; }
        public int FakultetID { get; set; }
        public TipKandidata TipKandidata { get; set; }
        public int TipKandidataID { get; set; }
        public string BrojIndeksa { get; set; }
        public CiklusStudija CiklusStudija { get; set; }
        public int CiklusStudijaID { get; set; }
        public GodinaStudija GodinaStudija { get; set; }
        public int GodinaStudijaID { get; set; }

        public Student Student { get; set; }
        public int? StudentID { get; set; }
        public  RezultatKonkursa RezultatKonkursa { get; set; }
        public int? RezultatKonkursaID { get; set; }
    }
}
