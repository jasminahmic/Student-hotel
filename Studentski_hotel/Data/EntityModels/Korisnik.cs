using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBdata.EntityModels
{
    public class Korisnik : IdentityUser
    {
        public Student Student { get; set; }

        public Admin Admin { get; set; }
        public Osoblje Osoblje { get; set; }
    }
}
