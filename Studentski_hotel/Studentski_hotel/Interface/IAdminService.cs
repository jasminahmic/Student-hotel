using DBdata.EntityModels;
using Studentski_hotel.Models.Admin;
using Studentski_hotel.Models.AdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Interface
{
    public interface IAdminService
    {
        AdminApiVM PrikazOsoblja();
        public Lista GetList();
        void AddOsobljaAsync(DodajOsobljeVM osoblje);
        DodajOsobljeVM GetOsoblja(int ID);
        void EditOsoblja(DodajOsobljeVM vm);

    }
}
