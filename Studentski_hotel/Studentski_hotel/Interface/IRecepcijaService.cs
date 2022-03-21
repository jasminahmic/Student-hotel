using DBdata.EntityModels;
using Studentski_hotel.Models.Admin;
using Studentski_hotel.Models.Recepcija;
using Studentski_hotel.Models.RecepcijaApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Interface
{
    public interface IRecepcijaService
    {
        RecepcijaApiVM PregledBlackListe();
        RecepcijaApiVM GetAllStudents();
        public ListaKategorija GetList();
        void SnimiRazlog(RecepcijaApiEditBlVM student);
        public bool SkloniStudenta(int studentID);

    }
}
