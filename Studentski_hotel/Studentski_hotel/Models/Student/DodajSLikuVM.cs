using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Studentski_hotel.Models.Student
{
    public class DodajSLikuVM
    {
        public IFormFile NovaSlika { get; set; }
        public string ImeSlike { get; set; }


    }
}
