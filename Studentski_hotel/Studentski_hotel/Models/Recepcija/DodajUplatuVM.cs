using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class DodajUplatuVM
    {
        public int uplataID { get; set; }
        public int NacinUplateID { get; set; }

        public int VrijednostUplate { get; set; }
        public int RecepcijaID { get; set; }

        public List<SelectListItem> Studenti { get; set; }
        public List<SelectListItem> NaciniUplate { get; set; }

        public int StudentID { get; set; }

        public string DatumUplate { get; set; }

    }
}
