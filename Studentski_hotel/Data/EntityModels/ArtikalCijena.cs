using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class ArtikalCijena
    {
        public int ID { get; set; }
        public float Cijena { get; set; }

        public Artikal Artikal { get; set; }
        public int ArtikalID { get; set; }
    }
}
