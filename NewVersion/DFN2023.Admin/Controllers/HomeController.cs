using DFN2023.Admin.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DFN2023.Admin.Controllers;
using DFN2023.Admin.Helpers;
using DFN2023.Entities.EF;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.Models;

namespace DFN2023.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminService _adminService;

        public HomeController(ILogger<HomeController> logger, IAdminService parametreService)
        {
            _logger = logger;
            _adminService = parametreService;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "Ana Sayfa";

            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");

            HttpContext.Session.SetString("username", usr.Name + " " + usr.Surname);

            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("culture")))
            //{
                
            //}

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public ActionResult GetUserProfile()
        {

            return PartialView("_userProfil");
        }

        public IActionResult SetLanguage(string c, string r)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(c)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) }
            );

            if (r.Length == 1)
            {
                r = "/" + c;
            }
            else if (r.Length == 3)
            {
                r = "/" + c;
            }
            else if (r.Length > 3)
            {
                //if (r.IndexOf("/") == 0)
                //{ r = "/" + c + r; }
                //else
                //{ 
                    r = "/" + c + r.Substring(3); 
                //}
                
            }
            else { }

            return LocalRedirect(r);
        }

        [HttpPost]
        public List<LanguageDTO> getLanguages()
        {
            return _adminService.getLanguageList();
        }

        //[HttpPost]
        //public Task<JsonResult> getLogins([FromBody] DtParameters dtParameters)
        //{
        //    int lang = getLang(CultureInfo.CurrentCulture.Name);
        //    var sonuc = _adminService.getLoginsDashboard(dtParameters, lang);
        //    return Task.FromResult(Json(sonuc));
        //}

        //[HttpPost]
        //public Task<JsonResult> getStaticContents([FromBody] DtParameters dtParameters)
        //{
        //    int lang = getLang(CultureInfo.CurrentCulture.Name);
        //    var sonuc = _adminService.getStaticContentPageDashboard(dtParameters, lang);
        //    return Task.FromResult(Json(sonuc));
        //}

        //[HttpPost]
        //public Task<JsonResult> getStaticContentGroups([FromBody] DtParameters dtParameters)
        //{
        //    int lang = getLang(CultureInfo.CurrentCulture.Name);
        //    var sonuc = _adminService.getStaticContentGrupPageDashboard(dtParameters, lang);
        //    return Task.FromResult(Json(sonuc));
        //}
    }
}