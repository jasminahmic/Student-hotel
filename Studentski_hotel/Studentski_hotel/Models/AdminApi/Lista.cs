using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.AdminApi
{
    public class Lista
    {
        public class Row
        {
            public int ID { get; set; }
            public string Naziv { get; set; }
            public int KantonID { get; set; }
        }
        public List<Row> Kanton { get; set; }
        public List<Row> Opstina { get; set; }
        public List<Row> Spolovi { get; set; }
    }
}
