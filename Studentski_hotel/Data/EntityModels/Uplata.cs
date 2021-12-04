using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Uplata
    {
        public int ID { get; set; }
        public string  Datum { get; set; }
        public float Stanje { get; set; }
        public NacinUplate NacinUplate { get; set; }
        public int NacinUplateID { get; set; }
        public Osoblje Osoblje { get; set; }
        public int OsobljeID { get; set; }
        public Ugovor Ugovor { get; set; }
        public int UgovorID { get; set; }
    }
}
