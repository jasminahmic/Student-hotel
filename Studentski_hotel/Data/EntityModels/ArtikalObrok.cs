using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class ArtikalObrok
    {
        public int ObrokID { get; set; }
        public Obrok Obrok { get; set; }

        public int ArtikalID { get; set; }
        public Artikal Artikal { get; set; }

    }
}
