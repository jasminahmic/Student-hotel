using Studentski_hotel.Models.Referent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Interface
{
    public interface IKonkursService
    {
        public void DodajStudenta(DetaljiPrikazPrijavaVM detaljiPrikazaVM);
    }
}
