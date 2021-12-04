using DBdata.EntityModels;
using Studentski_hotel.Models.AdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Interface
{
   public  interface IAdminService
    {
       AdminApiVM PrikazOsoblja();
    }
}
