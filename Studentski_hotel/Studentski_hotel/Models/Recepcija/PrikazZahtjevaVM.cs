using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class PrikazZahtjevaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string ImePrezime { get; set; }
            public string Zahtjev { get; set; }
            public string Datum { get; set; }
            public string Soba { get; set; }
        }
        public List<Row> Zahtjevi{ get; set; }
    }
}
