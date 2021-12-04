using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class DodajObavijestVM
    {
        public int obavijestID { get; set; }
        public string Naslov { get; set; }
        public string Text { get; set; }
        public int RecepcionerID { get; set; }

       public string datum_dodavanja { get; set; }



    }
}
