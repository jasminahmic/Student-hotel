using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Artikal
    {
   
        public int ID { get; set; }
        public string NazivArtikla { get; set; }
        public IList<ArtikalObrok> ArtikalObroks { get; set; }

    }
}
