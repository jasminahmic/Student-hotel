using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.AdminApi
{
    public class AdminApiVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Pozicija { get; set; }
            public string DatumZaposlenja { get; set; }
            public string DatumRodenja { get; set; }
        }
        public List<Row> Uposlenici { get; set; }
    }
}
