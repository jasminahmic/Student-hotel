using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Student
{
    public class PosaljiZahtjevVM
    {
        public int ID { get; set; }
        public int VrstaZahtjevaID { get; set; }
        public List<SelectListItem> VrstaZahtjeva { get; set; }
        public string Text { get; set; }
    }
}
