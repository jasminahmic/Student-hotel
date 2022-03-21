using DBdata.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Studentski_hotel.Data;
using Studentski_hotel.Interface;
using Studentski_hotel.Models.RecepcijaApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Services
{
    public class RecepcijaService : IRecepcijaService
    {
        private ApplicationDbContext _dbContext;
        private IEmailService _emailService;
        private UserManager<Korisnik> _userManager;

        public RecepcijaService(ApplicationDbContext context, IEmailService emailService, UserManager<Korisnik> userManager)
        {
            _dbContext = context;
            _emailService = emailService;
            _userManager = userManager;
        }
        public RecepcijaApiVM PregledBlackListe()
        {
            RecepcijaApiVM lista = new RecepcijaApiVM();
            lista.studenti = _dbContext.Students.Where(x => x.BlackListID != null).Select(s => new RecepcijaApiVM.Row
            {
                ImeStudenta = s.Ime + " " + s.Prezime,
                Razlog = s.RazlogZaBlackListu,
                studentID = s.ID

            }).ToList();

            return lista;
        }

        public RecepcijaApiVM GetAllStudents()
        {
            RecepcijaApiVM lista = new RecepcijaApiVM();
            lista.studenti = _dbContext.Students.Select(s => new RecepcijaApiVM.Row
            {
                ImeStudenta = s.Ime + " " + s.Prezime,
                Razlog = s.RazlogZaBlackListu,
                studentID = s.ID

            }).ToList();

            return lista;
        }



        public ListaKategorija GetList()
        {
            var vm = new ListaKategorija();
            var kantoni = _dbContext.BlackLists.Select(a => new ListaKategorija.Row
            {
                ID = (int) a.ID,
                Naziv = a.Naziv
            }).ToList();

            vm.Kategorije = kantoni;

            return vm;
        }
        public void SnimiRazlog(RecepcijaApiEditBlVM noviStudent)
        {
            var student = _dbContext.Students.Where(x => x.ID == noviStudent.StudentID).FirstOrDefault();
            student.RazlogZaBlackListu = noviStudent.Razlog;
            student.BlackListID = noviStudent.BlackListID;
            _dbContext.SaveChanges();
        }

        public bool SkloniStudenta(int studentID)
        {
            var student = _dbContext.Students.Where(x => x.ID == studentID).FirstOrDefault();

            student.RazlogZaBlackListu = null;
            student.BlackListID = null;
            _dbContext.SaveChanges();

            return true;
        }

    }
}
