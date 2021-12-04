using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class NajavaOdlaska
    {
        public int ID { get; set; }
        public string DatumPolaska { get; set; }
        public string DatumPovratka { get; set; }
        public Ugovor Ugovor { get; set; }
        public int UgovorID { get; set; }
    }
}
