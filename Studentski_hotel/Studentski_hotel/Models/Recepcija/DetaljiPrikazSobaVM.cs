using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class DetaljiPrikazSobavm
    {
        public int ID { get; set; }
        public string Sprat { get; set; }
        public string Broj_Sobe { get; set; }
        public string bROJ_Kreveta { get; set; }
        public string ImaBalkon { get; set; }
        public string Napomena { get; set; }
        public bool Popunjena { get; set; }
        public class Studenti
        {
            public int ID { get; set; }
            public string Ime { get; set; }
        }
        public List<Studenti> studenti{ get; set; }
    }
}
