using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class PregledSobaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string BrojSobe { get; set; }

            public string BrojKreveta { get; set; }
            public string Studenti { get; set; }
            public bool Popunjena { get; set; }
        }
        public List<Row> Sobe { get; set; }
    }
}
