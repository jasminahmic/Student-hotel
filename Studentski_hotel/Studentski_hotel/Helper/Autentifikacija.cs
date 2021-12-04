using System.Linq;
using DBdata.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Studentski_hotel.Data;

namespace Studentski_hotel.Helper
{
    public static class Autentifikacija
    {
        public static Korisnik LogiraniKorisnik(this HttpContext httpContext)
        {
            //Preuzimamo DbContext preko app services
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            //Preuzimamo userManager preko app services
            UserManager<Korisnik> userManager = httpContext.RequestServices.GetService<UserManager<Korisnik>>();

            if (httpContext.User == null)
                return null;

            //TrenutniKorisnikID
            string userId = userManager.GetUserId(httpContext.User);

            Korisnik k = db.Korisniks.Where(s => s.Id == userId)
                .Include(s => s.Osoblje)
                .Include(s => s.Student)
                .Include(s => s.Admin)
                .SingleOrDefault();

            return k;
        }
    }
}
