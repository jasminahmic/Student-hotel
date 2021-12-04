using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Obavijest
    {
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string Text { get; set; }
        public string DatumVrijeme { get; set; }
        public Osoblje Osoblje { get; set; }
        public int OsobljeID { get; set; }

    }
}
