using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
   public class Zahtjev
    {
        public int ID { get; set; }
        public string Datum { get; set; }
        public string Text { get; set; }
        public VrstaZahtjeva VrstaZahtjeva { get; set; }
        public int VrstaZahtjevaID { get; set; }
        public VrstaStanjaZahtjeva VrstaStanjaZahtjeva { get; set; }
        public int VrstaStanjaZahtjevaID { get; set; }
        public Ugovor Ugovor { get; set; }
        public int UgovorID { get; set; }
    }
}
