using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class PregledUplataVM
    {
        public class Row
        {
            public int uplataID { get; set; }
            public string ImeRecepcionera { get; set; }
            public string DatumUplate { get; set; }
            public string Iznos { get; set; }

            public string NacinUplate { get; set; }
        }

        public List<Row> uplateStudenta { get; set; }
    }
}