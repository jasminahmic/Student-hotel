using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Upozorenje
    {
        public int ID { get; set; }
        public string DatumSlanja { get; set; }
        public string Text { get; set; }
        public Ugovor Ugovor { get; set; }
        public int UgovorID { get; set; }
    }
}
