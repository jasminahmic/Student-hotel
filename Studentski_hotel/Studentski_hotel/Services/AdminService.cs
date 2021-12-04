using DBdata.EntityModels;
using Studentski_hotel.Data;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.AdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Services
{
    public class AdminService:IAdminService
    {
        private ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }
      public  AdminApiVM PrikazOsoblja()
        {
            var rows = _context.Osobljes.Select(a => new AdminApiVM.Row
            {
                ID = a.ID,
                Ime = a.Ime,
                Prezime = a.Prezime,
                Pozicija = a.Rola.Naziv,
                DatumRodenja = a.DatumRodjenja,
                DatumZaposlenja = a.DatumZaposlenja
            }).ToList();
            AdminApiVM model = new AdminApiVM();
            model.Uposlenici = rows;
            return model;
        }
    }
}
