using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Models.RecepcijaApi
{
    public class RecepcijaApiEditBlVM
    {
        public int StudentID { get; set; }
        public int BlackListID { get; set; }
        public List<SelectListItem> BlackList { get; set; }
        public List<SelectListItem> Studenti { get; set; }
        public string Razlog { get; set; }
    }
}
