using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Konkurss
{
    public class KonkursPodaciVM
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string ImeOca { get; set; }
        public List<SelectListItem> MjestoRodjenja;
        public int MjestoRodjenjaID { get; set; }

        public string ZanimanjeRoditelja { get; set; }
        public List<SelectListItem> Pol;
        public int PolID { get; set; }
        public string JMBG { get; set; }
        public string LicnaKarta { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Mobitel { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public List<SelectListItem> MjestoStanovanja;
        public int MjestoStanovanjaID { get; set; }

        public List<SelectListItem> Fakultet;
        public int FakultetID { get; set; }

        public List<SelectListItem> TipKandidata;
        public int TipKandidataID { get; set; }

        public List<SelectListItem> Kanton;
        public int KantonID { get; set; }

        public string BrojIndeksa { get; set; }
        public List<SelectListItem> CiklusStudija;
        public int CiklusStudijaID { get; set; }

        public List<SelectListItem> GodinaStudija;
        public int GodinaStudijaID { get; set; }

    }
}
