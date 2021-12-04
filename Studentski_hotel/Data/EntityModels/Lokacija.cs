using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Lokacija
    {
        public int ID { get; set; }
        public string Adresa { get; set; }
        public string PostanskiBroj { get; set; }
        public Grad MjestoStanovanja { get; set; }
        public int MjestoStanovanjaID { get; set; }
        public Kanton Kanton { get; set; }
        public int KantonID { get; set; }
    }
}
