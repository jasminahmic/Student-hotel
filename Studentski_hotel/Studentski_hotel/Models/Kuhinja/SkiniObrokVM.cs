using DBdata.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Kuhinja
{
    public class SkiniObrokVM
    {
        public int obrokID { get; set; }
        public string brojKartice { get; set; }
        public List<int> SelectedArtikals { get; set; }

        public float IznosObroka { get; set; }
    }
}
