using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
   public class Osoblje
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public string GodinaZaposlenja { get; set; }
        public string DatumZaposlenja { get; set; }
 
        public Lokacija Lokacija { get; set; }
        public int LokacijaID { get; set; }
        public Pol Pol { get; set; }
        public int PolID { get; set; }
        public Rola Rola { get; set; }
        public int RolaID { get; set; }
        public Korisnik Korisnik { get; set; }
        public string KorisnikID { get; set; }
    }
}
