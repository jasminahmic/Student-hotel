using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Controllers
{
    [Route("/[controller]")]
    [ApiController]

    public class AdminApiController : ControllerBase
    {
        private IAdminService _adminservice;
        public AdminApiController(IAdminService adminservice)
        {
            _adminservice = adminservice;
        }
        [HttpGet]
        [Route("PrikazOsoblja")]
        public IActionResult PrikazOsoblja()
        {
            return Ok(_adminservice.PrikazOsoblja());
        }
        [HttpGet]
        [Route("GetLista")]
        public IActionResult GetLista()
        {
            return Ok(_adminservice.GetList());
        }
        [HttpPost]
        [Route("AddOsoblja")]
        public IActionResult AddOsoblja(DodajNastavnikaVM osoblje)
        {
            _adminservice.AddOsobljaAsync(osoblje);
            return Ok();
        }
        [HttpPut]
        [Route("EditAdmina")]
        public IActionResult EditAdmina(DodajNastavnikaVM osoblje)
        {
            _adminservice.EditOsoblja(osoblje);
            return Ok();
        }
        [HttpGet]
        [Route("GetOsoblje")]
        public IActionResult GetAdmina(int id)
        {
            return Ok(_adminservice.GetOsoblja(id));
        }
    }
}
}
