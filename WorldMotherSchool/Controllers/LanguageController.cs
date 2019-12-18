using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WorldMotherSchool.Language;

namespace WorldMotherSchool.Controllers
{
    public class LanguageController : Controller
    {
        public LanguageController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
             var cultureLang = SupportedLanguage.GetUILanguage(culture);
             var url = "";
             if(returnUrl == "~/")
            {
                url = "~/" + cultureLang;
            }
             else
            {
                var ss = returnUrl.Substring(2, 2);
                url = returnUrl.Replace(ss,cultureLang);
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureLang)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(url);
        }
    }
}