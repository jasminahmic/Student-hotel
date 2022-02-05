using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class FilterStudenataNeplacenoVM
    {
        public class Row
        {
            public int studentID { get; set; }
            public string ImeStudenta { get; set; }
            public string Mjesec { get; set; }
        }

        public string CurrentDate { get; set; }
        public List<Row> studentiNisuUplatili { get; set; }
    }
}
