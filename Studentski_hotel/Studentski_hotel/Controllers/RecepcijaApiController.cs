using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.Recepcija;
using Studentski_hotel.Models.RecepcijaApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RecepcijaApiController : ControllerBase
    {
        private IRecepcijaService _recepcijaservice;
        public RecepcijaApiController(IRecepcijaService recepcijaservice)
        {
            _recepcijaservice = recepcijaservice;
        }
        [HttpGet]
        [Route("PregledBlackListe")]
        public IActionResult PregledBlackListe()
        {
            return Ok(_recepcijaservice.PregledBlackListe());
        }
        [HttpGet]
        [Route("GetList")]
        public IActionResult Lista()
        {
            return Ok(_recepcijaservice.GetList());
        }

        [HttpPut]
        [Route("EditBlackList")]
        public IActionResult SnimiRazlog(RecepcijaApiEditBlVM student)
        {
            _recepcijaservice.SnimiRazlog(student);
            return Ok();
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            return Ok(_recepcijaservice.GetAllStudents());
        }

        [HttpDelete("{id}")]
        [Route("SkloniStudenta")]
        public bool SkloniStudenta(int studentID)
        {
            _recepcijaservice.SkloniStudenta(studentID);
            return true;
        }
    }
}
