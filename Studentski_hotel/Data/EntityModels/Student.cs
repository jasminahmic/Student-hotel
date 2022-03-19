using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Student
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeOca { get; set; }
        public Grad MjestoRodjenja { get; set; }
        public int MjestoRodjenjaID { get; set; }
        public string ZanimanjeRoditelja { get; set; }
        public Pol Pol { get; set; }
        public int  PolID { get; set; }
        public string JMBG { get; set; }
        public string Slika { get; set; }

        public string LicnaKarta { get; set; }
        public string DatumRodjenja { get; set; }
        public string Mobitel { get; set; }
        public string Email { get; set; }

        public Fakultet Fakultet { get; set; }
        public int FakultetID { get; set; }
        public TipKandidata TipKandidata { get; set; }
        public int TipKandidataID { get; set; }
        public string BrojIndeksa { get; set; }
        public CiklusStudija CiklusStudija { get; set; }
        public int CiklusStudijaID { get; set; }
        public GodinaStudija GodinaStudija { get; set; }
        public int GodinaStudijaID { get; set; }

        public Lokacija Lokacija { get; set; }
        public int LokacijaID { get; set; }


        public bool Uselio { get; set; }
        public Korisnik Korisnik { get; set; }
        public string KorisnikID { get; set; }

        public BlackList BlackList { get; set; }
        public int? BlackListID { get; set; }

        public string RazlogZaBlackListu { get; set; }
    }
}
