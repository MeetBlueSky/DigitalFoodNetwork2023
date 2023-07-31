using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Web.Models;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace DFN2023.Web.Controllers
{
    public class HakkimizdaController : BaseController
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IWebsiteService _websiteService;
        private readonly IHtmlLocalizer<HakkimizdaController> _localizer;

        public HakkimizdaController(IWebsiteService _p, IHtmlLocalizer<HakkimizdaController> localizer)
        {
            _websiteService = _p;
            _localizer = localizer;
        }



        [HttpGet, Route("/{languageinurl}/about/{title}/{id}", Name = "about/title/id"),
            Route("/{languageinurl}/hakkimizda/{title}/{id}", Name = "hakkimizda/title/id")]
        public IActionResult Detay(int id, string title, string languageinurl)
        {
            ViewData["WP"] = "Hakkımızda";
            if (id > 0)
            {


                setLang(languageinurl);
                int lang = getLang(CultureInfo.CurrentCulture.Name);
                // int lang = getLang(languageinurl);
                PublicModel sm = new PublicModel();
                sm.StaticContentPageList = _websiteService.getStaticContentByGrupId(id, lang);
                sm.StaticContentGrupPage = _websiteService.getStaticGrup(id, lang);
                ViewBag.PageTitle = sm.StaticContentGrupPage.Title;
                return View(sm);
            }
            else
            {
                return LocalRedirect("/" + CultureInfo.CurrentCulture + "/");
            }

            /*
            ViewData["WP"] = "Topluluk";
            if (id > 0)
            {
                int lang = getLang(CultureInfo.CurrentCulture.Name);
                StaticModel sm = new StaticModel();
                sm.StaticContentPage = _websiteService.getStaticPage(id, lang);
                int GrupId = ViewBag.GrupPageId = sm.StaticContentPage.GrupId;
                sm.StaticContentPageList = _websiteService.getStaticContentByTempId(1, lang).Where(p => p.GrupId == GrupId).ToList();
                
                return View(sm);

            }
            else
            {
                return LocalRedirect("/" + CultureInfo.CurrentCulture + "/Home");
            }
            */
        }





    }
}