using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Admin
{
    public class PrikazOsobljaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string DatumRodjenja { get; set; }
        }
    public List<Row> Osoblje { get; set; }
    }
}
