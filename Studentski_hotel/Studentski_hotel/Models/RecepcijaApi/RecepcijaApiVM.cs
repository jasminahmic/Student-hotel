using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.RecepcijaApi
{
    public class RecepcijaApiVM
    {
        public class Row
        {
            public int studentID { get; set; }
            public string ImeStudenta { get; set; }
            public string Razlog { get; set; }

        }

        public List<Row> studenti { get; set; }
    }
}
