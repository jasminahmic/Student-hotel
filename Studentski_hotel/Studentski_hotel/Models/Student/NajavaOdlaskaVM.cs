using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Student
{
    public class NajavaOdlaskaVM
    {
        public int ID { get; set; }
        public DateTime DatumPolaska { get; set; }
        public DateTime DatumPovratka { get; set; }
        public int UgovorID { get; set; }
    }
}
