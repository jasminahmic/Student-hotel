using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Kuhinja
{
    public class PrikazKarticaVM
    {
        public class Row
        {
            public int karticaID { get; set; }
            public string BrojKartice { get; set; }
            public int StanjeKartice { get; set; }
        }

        public List<Row> kartice { get; set; }
    }
}
