using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Admin
    {

        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public Korisnik Korisnik { get; set; }
        public string KorisnikID { get; set; }
    }
}
