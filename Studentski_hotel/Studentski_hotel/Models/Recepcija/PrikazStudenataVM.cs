using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class PrikazStudenataVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Soba { get; set; }
            public string BrojKartice { get; set; }
            public bool Uselio { get; set; }

        } 
        public List<Row> Studenti { get; set; }
    }

}
