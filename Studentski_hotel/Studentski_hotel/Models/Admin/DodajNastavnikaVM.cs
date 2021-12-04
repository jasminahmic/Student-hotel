using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Admin
{
    public class DodajNastavnikaVM
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public List<SelectListItem> Pol;
        public int PolID { get; set; }
        public string JMBG { get; set; }
        public string LicnaKarta { get; set; }
        //public string Mjesto_izdavanja_LK { get; set; }
        public DateTime DatumRodjenja { get; set; }

        public DateTime DatumZaposlenja { get; set; }

        public string Adresa { get; set; }
        public List<SelectListItem> MjestoStanovanja;
        public int MjestoStanovanjaID { get; set; }
        public string PostanskiBroj { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public string mobitel { get; set; }
        public string KorisnikID { get; set; }
        public int LokacijaID { get; set; }
        public int TipKorisnika { get; set; }




        public List<SelectListItem> Kanton;
        public int KantonID { get; set; }


    }
}
