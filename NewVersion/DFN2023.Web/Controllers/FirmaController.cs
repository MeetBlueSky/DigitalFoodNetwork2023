using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using DFN2023.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFN2023.Web.Controllers
{
    public class FirmaController : Controller
    {
        private readonly IWebsiteService _websiteService;
        public FirmaController(IWebsiteService parametreService)
        {
            _websiteService = parametreService;
        }
        public IActionResult Detay(int id)
        {
            PublicModel pm = new PublicModel();
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");

            
                var a = _websiteService.getCompanyDetay(id, usr == null ? 0 : usr.Id);

                if (a !=null)
            {
                pm.sirket = a;
                pm.urunler = _websiteService.getUrunlerList(a.Id);
                    pm.user = usr;
                    return View(pm);
                }
                else
                {
                    return RedirectToAction("Home", "Index");
                }


        }
    }
}
