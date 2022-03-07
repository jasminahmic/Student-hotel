using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentski_hotel.Helper
{
    public static class DisabledButton
    {
        public static HtmlString DisabledIf(bool condition)
        {
            return new HtmlString(condition ? "disabled=\"disabled\"" : "");
        }
    }
}
