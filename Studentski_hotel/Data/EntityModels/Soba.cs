using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Soba
    {
        public int ID { get; set; }
        public string Sprat { get; set; }
        public string BrojSobe { get; set; }
        public int BrojKreveta { get; set; }
        public bool ImaBalkon { get; set; }
        public string Napomena { get; set; }
    }
}
