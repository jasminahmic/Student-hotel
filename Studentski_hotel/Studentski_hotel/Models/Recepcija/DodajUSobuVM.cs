using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{

    public class DodajUSobuVM
    {
        public int ID { get; set; }
        public List<SelectListItem> Studenti { get; set; }
        public int StudentID { get; set; }
        public List<SelectListItem> Soba { get; set; }
        public int SobaID { get; set; }
        public List<SelectListItem> BrojKartice { get; set; }
        public int BrojKarticeID { get; set; }
        public string DatumUseljenja { get; set; }
        public string Password { get; set; }
    }

}
