using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.RecepcijaApi
{
    public class ListaKategorija
    {
        public class Row
        {
            public int ID { get; set; }
            public string Naziv { get; set; }
        }
        public List<Row> Kategorije { get; set; }
    }
}
