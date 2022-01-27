using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.Recepcija
{
    public class DetaljiUplateVM
    {
        public int uplataID { get; set; }
        public string ImeRecepcionera { get; set; }
        public string Student { get; set; }
        public string DatumUplate { get; set; }

        public string NacinUplate{ get; set; }
        public string VrijednostUplate { get; set; }
        public string RazlogUplate { get; set; }

    }
}
