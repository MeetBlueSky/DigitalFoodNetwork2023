using DFN2023.Contracts;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using DFN2023.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFN2023.Web.Controllers
{
    public class TedarikciController : Controller
    {
        private readonly IWebsiteService _websiteService;
        public TedarikciController(IWebsiteService parametreService)
        {
            _websiteService = parametreService;
        }
        public IActionResult List()
        {
            int kid = Convert.ToInt32(HttpContext.Session.GetInt32("kategoriid"));
            string ürün = HttpContext.Session.GetString("tedarikciadi");
            if (kid!=0)
            {
                var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
                PublicModel pm = new PublicModel();
                pm.user = usr;

                var kategorilist = _websiteService.getCategoryList();
                pm.kategoriler = kategorilist;
                pm.skategoriid = kid;
                pm.tedarikciadi = ürün;

                var filtersonuc = _websiteService.getTedarik(kid, ürün);
                pm.tedariklist = filtersonuc;
                return View(pm);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
          
        }
        public IActionResult Harita(int id)
        {

            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            pm.user = usr;
            return View(pm);
        }
        }
}
