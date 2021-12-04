using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studentski_hotel.Interface;
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
    }
}
