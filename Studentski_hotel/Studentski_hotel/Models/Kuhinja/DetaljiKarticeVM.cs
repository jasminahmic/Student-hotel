using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Kuhinja
{
    public class DetaljiKarticeVM
    {
        public int karticaID { get; set; }
        public int StudentID { get; set; }
        public string StudentIme { get; set; }
        public string BrojKartice { get; set; }

        public string TipStudenta { get; set; }
        public string StanjeKartice { get; set; }
        public bool RedFlag { get; set; }
    }
}
