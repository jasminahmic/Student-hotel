using DBdata.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Studentski_hotel.Data;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.Konkurss;
using Studentski_hotel.Models.Referent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Controllers
{
    //[Route("[controller]")]
    //[ApiController]

    [ApiController]
    [Route("[controller]/[action]")]
    public class KonkursApiController : ControllerBase
    {
        private IKonkursService _konkursService;
        private ApplicationDbContext dbContext;
        public KonkursApiController(IKonkursService konkursService, ApplicationDbContext _db)
        {
            _konkursService = konkursService;
            dbContext = _db;
        }

        [HttpGet]
        public ActionResult GetOpstine()
        {
            return Ok(_konkursService.GetOpstine());
        }

        [HttpGet("{KantonID}")]
        public ActionResult GetOpstineByKanton(int KantonID)
        {
            return Ok(_konkursService.GetOpstineByKanton(KantonID));
        }

        [HttpGet]
        public ActionResult GetKanton()
        {
            return Ok(_konkursService.GetKanton());
        }
        
        [HttpGet]
        public ActionResult GetCiklusStudija()
        {
            return Ok(_konkursService.GetCiklusStudija());
        }

        [HttpGet]
        public ActionResult GetTipKandidata()
        {
            return Ok(_konkursService.GetTipKandidata());
        }

        [HttpGet]
        public ActionResult GetFakultet()
        {
            return Ok(_konkursService.GetFakultet());
        }
        
        [HttpGet]
        public ActionResult GetGodinaStudija()
        {
            return Ok(_konkursService.GetGodinaStudija());
        }

        [HttpGet]
        public ActionResult GetPol()
        {
            return Ok(_konkursService.GetPol());
        }

        [HttpPost]
        public ActionResult<Konkurs> SnimiZahtjev([FromBody] KonkursPodaciVM noviStudent)
        {
            //_konkursService.SnimiZahtjev(noviStudent);
            //return Ok();
            var konkurs = new Konkurs()
            {
                Ime = noviStudent.Ime,
                Prezime = noviStudent.Prezime,
                ImeOca = noviStudent.ImeOca,
                MjestoRodjenjaID = noviStudent.MjestoRodjenjaID,
                ZanimanjeRoditelja = noviStudent.ZanimanjeRoditelja,
                PolID = noviStudent.PolID,
                JMBG = noviStudent.JMBG,
                LicnaKarta = noviStudent.LicnaKarta,
                DatumRodjenja = noviStudent.DatumRodjenja.ToString(),
                Mobitel = noviStudent.Mobitel,
                Email = noviStudent.Email,

                Adresa = noviStudent.Adresa,
                MjestoStanovanjaID = noviStudent.MjestoStanovanjaID,
                KantonID = noviStudent.KantonID,

                FakultetID = noviStudent.FakultetID,
                TipKandidataID = noviStudent.TipKandidataID,
                BrojIndeksa = noviStudent.BrojIndeksa,
                CiklusStudijaID = noviStudent.CiklusStudijaID,
                GodinaStudijaID = noviStudent.GodinaStudijaID
            };

            dbContext.Add(konkurs);
            dbContext.SaveChanges();
            return konkurs;
        }
    }
}
