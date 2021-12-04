using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DBdata.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Studentski_hotel.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool dozvoljenoStudentu, bool dozvoljenoRecepcioneru,bool dozvoljenoReferentu,bool dozvoljenoAdminu)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { dozvoljenoStudentu, dozvoljenoRecepcioneru, dozvoljenoReferentu, dozvoljenoAdminu };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        public MyAuthorizeImpl(bool dozvoljenoStudentu, bool dozvoljenoRecepcioneru, bool dozvoljenoReferentu,bool dozvoljenoAdminu)
        {
            _dozvoljenoStudentu = dozvoljenoStudentu;
            _dozvoljenoRecepcioneru = dozvoljenoRecepcioneru;
            _dozvoljenoReferentu = dozvoljenoReferentu;
            _dozvoljenoAdminu = dozvoljenoAdminu;
        }
        private readonly bool _dozvoljenoStudentu;
        private readonly bool _dozvoljenoRecepcioneru;
        private readonly bool _dozvoljenoReferentu;
        private readonly bool _dozvoljenoAdminu;

        public void OnActionExecuted(ActionExecutedContext context)
        {


        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext httpContext = filterContext.HttpContext;

            Korisnik k = httpContext.LogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["PorukaError"] = "Niste logirani";
                }
                filterContext.Result = new RedirectResult("/Identity/Account/Login");
                return;
            }

            //KretanjePoSistemu.Save(httpContext);

            //studenti mogu pristupiti 
            if (_dozvoljenoStudentu && k.Student != null)
            {
                return;//ok - ima pravo pristupa
            }

            //nastavnici mogu pristupiti 
            if (_dozvoljenoRecepcioneru && k.Osoblje != null && k.Osoblje.RolaID==1)
            {
                return;//ok - ima pravo pristupa
            }
            if (_dozvoljenoReferentu && k.Osoblje != null && k.Osoblje.RolaID==2)
            {
                return;//ok - ima pravo pristupa
            }
            if (_dozvoljenoAdminu && k.Admin != null)
            {
                return;//ok - ima pravo pristupa
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["PorukaError"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectResult("/");
        }
    }
}
