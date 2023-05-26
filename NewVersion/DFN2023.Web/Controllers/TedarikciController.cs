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

		public IActionResult Duzenle()
		{
			var usrses = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
			PublicModel pm = new PublicModel();


			if (usrses != null)
			{
				var companyinfo = _websiteService.getCompanyInfo(usrses.Id);
                if (companyinfo!=null)
				{
					pm.ulkeler = _websiteService.getCountryList();
					pm.sirkettipleri = _websiteService.getCompanyTypeList();
					pm.userbilg = usrses;
					pm.user = usrses;
					pm.sirket = companyinfo;
					return View(pm);

                }
                else
                {
					return RedirectToAction("TedMailOnaylama", "Login", new { code = usrses.EmailConfirmed });
				}
			}

			else
			{
				return RedirectToAction("Index", "Home");
			}

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
    
    public IActionResult UrunEkle()
    {
        PublicModel pm = new PublicModel();
        var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");

            if (usr != null)
            {
                int a = _websiteService.getCompanyId(usr.Id);
                
                if (a>0)
                {
                    var kategorilist = _websiteService.getCategoryList();
                    pm.kategoriler = kategorilist;
                    pm.urunler = _websiteService.getUrunlerList(a);
                    if (a > 0)
                    {
                        HttpContext.Session.SetInt32("selectcompid", a);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    pm.user = usr;
                    return View(pm);
                }
                else
                {
                    return RedirectToAction("TedMailOnaylama", "Login", new { code = usr.EmailConfirmed });
                }

                return RedirectToAction("Index", "Home");
            }

        else
        {
            return RedirectToAction("Index", "Home");
        }
    }


    [HttpPost]
    public Task<JsonResult> createUrun(ProductCompanyDTO comp)
    {
        try
        {
            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            var selectcompid = Convert.ToInt32(HttpContext.Session.GetInt32("selectcompid"));

            comp.LastUpdateDate = DateTime.Now;
            comp.LastUpdatedBy = usr.Id;
            comp.LastUpdatedBy = usr.Id;
            comp.LastIP = HttpContext.Connection.RemoteIpAddress?.ToString();


                if (comp.Id <= 0)
            {
                comp.CreateDate = DateTime.Now;
                comp.CreatedBy = usr.Id;
                comp.LastIP= HttpContext.Connection.RemoteIpAddress?.ToString();
                comp.Status = 1;
                comp.ProductBaseId = 1;
                comp.CompanyId = selectcompid;
                comp.RowNum = 1;
            }

            var result = _websiteService.createUrun(comp);
            if (result != null)
            {
                sonuc = new { hata = false, mesaj = "İşlem Başarılı", res = "" };

            }
            else
            {
                sonuc = new { hata = true, mesaj = "Hata Oluştu", res = "" };
            }


            return Task.FromResult(Json(sonuc));
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost]
    public Task<JsonResult> deleteUrun(ProductCompany comp)
    {


        var sonuc = new { hata = true, mesaj = "Error", res = "" };
        try
        {

            if (comp != null)
            {
                var result = _websiteService.deleteUrun(comp.Id);
                if (result)
                {

                    sonuc = new { hata = false, mesaj = "Başarılı", res = "" };
                }
                else
                {
                    sonuc = new { hata = true, mesaj = "Hata tespit edildi", res = "" };

                }

            }
            else
            {
                sonuc = new { hata = true, mesaj = "Hata tespit edildi", res = "" };

            }



        }
        catch (Exception e)
        {
            sonuc = new { hata = true, mesaj = "Hata tespit edildi", res = "" };
        }

        return Task.FromResult(Json(sonuc));

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
                            sonuc = new { hata = false, mesaj = "Favoriden çıkarıldı", res = "" };


                        }
                        else
                        {
                            sonuc = new { hata = false, mesaj = "Favorilere eklendi", res = "" };

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
