using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Studentski_hotel.Models.Student
{
    public class LicniPodaciVM
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeOca { get; set; }

        public string Prebivaliste { get; set; }
        public string BrLicneKarte { get; set; }
        public string DatumRodjenja { get; set; }
        public string BrojIndeksa { get; set; }

        public string BrMobitela { get; set;  }

        public string SlikaStanara { get; set; }

        public string TipStanara { get; set; }





    }
}
