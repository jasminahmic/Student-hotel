using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Studentski_hotel.Models.Konkurss;
using Studentski_hotel.Models.Referent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Studentski_hotel.Interface
{
    public interface IKonkursService
    {
        public List<SelectListItem> GetOpstine();
        public List<SelectListItem> GetOpstineByKanton(int KantonID);
        public List<SelectListItem> GetKanton();
        public List<SelectListItem> GetPol();
        public List<SelectListItem> GetCiklusStudija();
        public List<SelectListItem> GetTipKandidata();
        public List<SelectListItem> GetFakultet();
        public List<SelectListItem> GetGodinaStudija();
        public KonkursPodaciVM KonkursForma();
        public void SnimiZahtjev(KonkursPodaciVM noviStudent);
        public void DodajStudenta(DetaljiPrijavaVM detaljiPrikazaVM); 
    }
}
