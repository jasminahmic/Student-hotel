using DBdata.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Kuhinja
{
    public class DetaljiKarticeVM
    {
        public int karticaID { get; set; }
        public int StudentID { get; set; }
        public string StudentIme { get; set; }
        public string BrojKartice { get; set; }

        public string TipStudenta { get; set; }
        public float StanjeKartice { get; set; }
        public bool RedFlag { get; set; }
        // za lijevu formu data
        public int ObrokID { get; set; }
        public string Datum { get; set; }

        public float IznosObroka { get; set; }

        public List<ArtikalObrok> DetaljiObroka { get; set; }

        public MultiSelectList PonudaArtikli { get; set; }

        public int OsobljeID { get; set; }

        public Osoblje Osoblje { get; set; }
    }
}
