using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Ugovor
    {
        public int ID { get; set; }
        public Soba Soba { get; set; }
        public int SobaID { get; set; }  
        public Student Student { get; set; }
        public int StudentID { get; set; }
        public Kartica Kartica { get; set; }
        public int KarticaID { get; set; }
        public Osoblje DodanUgovorOsoblje { get; set; }
        public int DodanUgovorOsobljeID { get; set; }
        public string DatumUseljenja { get; set; }
        public string DatumIseljenja { get; set; }

    }
}
