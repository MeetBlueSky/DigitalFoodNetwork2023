using DFN2023.Contracts;
using DFN2023.Entities.DTO;
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
            List<ProductCompanyDTO> filtersonuc = new();
           
                var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
                PublicModel pm = new PublicModel();
                pm.user = usr;

                var kategorilist = _websiteService.getCategoryList();
                pm.kategoriler = kategorilist;
                pm.skategoriid = kid;
                pm.tedarikciadi = ürün;
                pm.mapkoor = _websiteService.getCompanyMap();

                if (pm.user!=null)
                {
                     filtersonuc = _websiteService.getTedarik(kid, ürün, usr.Id);

				}
				else
                {
                     filtersonuc = _websiteService.getTedarik(kid, ürün, 0);

                }
                pm.tedariklist = filtersonuc;
                return View(pm);

          
          
        }
        public IActionResult Harita(int id)
        {

            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            pm.user = usr;
            return View(pm);
        }
        [HttpPost]
        public Task<JsonResult> favEkleCikar(int companyid, int durum)
        {
            try
			{

                var sonuc = new { hata = true, mesaj = "Error", res = "" };
                var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
                if (usr.Id> 0)
				{
                    var dgr = _websiteService.favMethod(companyid, usr.Id, durum);
                    if (dgr)
                    {
                        if (durum > 0)
                        {
                            sonuc = new { hata = false, mesaj = "Favorilere eklendi", res = "" };

                        }
                        else
                        {
                            sonuc = new { hata = false, mesaj = "Favoriden çıkarıldı", res = "" };

                        }
                    }
                    else
                    {
                        sonuc = new { hata = true, mesaj = "Hata Oluştu", res = "" };
                    }
				}
				else
				{

                    sonuc = new { hata = true, mesaj = "Giriş Yapın", res = "" };
                }
               
                return Task.FromResult(Json(sonuc));
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
