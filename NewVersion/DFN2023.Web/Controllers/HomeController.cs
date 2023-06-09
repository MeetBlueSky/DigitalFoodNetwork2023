﻿using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using DFN2023.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace DFN2023.Web.Controllers
{
    public class HomeController : BaseController
    {
        
        private readonly IWebsiteService _websiteService;
        public HomeController(IWebsiteService parametreService)
        {
            _websiteService = parametreService;
        }
        public IActionResult Index()
        {

            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            pm.user = usr;

            var kategorilist = _websiteService.getCategoryList();
            pm.kategoriler = kategorilist;
            var sirketlist = _websiteService.getCompanyList();
            pm.sirketler = sirketlist;
            var a = _websiteService.getAnasayfaList();
            if (a.Count>0)
            {

                pm.stanasayfa = a; 
                pm.stanasayfatuk = a.First(x => x.GrupTempId == 3);
                pm.stanasayfated = a.First(x => x.GrupTempId == 4);


                pm.stanasayfasec1 = a.First(x => x.GrupTempId == 6);
                pm.stanasayfasec2 = a.First(x => x.GrupTempId == 1);
            }
           

            return View(pm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public void filterTedarik(int kid, string urun)
        {

            HttpContext.Session.SetInt32("kategoriid", kid);
            HttpContext.Session.SetString("tedarikciadi", urun==null?"":urun);
        }
        [HttpPost]
        public async Task<JsonResult> kayitOl(User user)
        {

            var sonuc = new { hata = true, mesaj = "Teknik Ariza", res = "/" };
            try
            {
                user.CreateDate = DateTime.Now;
                user.Password = XamarinUtils.Sifrele(user.Password);
                user.LastIP = HttpContext.Connection.RemoteIpAddress?.ToString();
                var usr = _websiteService.createUser(user);
                if (usr!=null)
                {
                    if (usr.Id>0)
                    {

                        HttpContext.Session.SetString("emailkodu", usr.EmailConfirmed);
                        HttpContext.Session.SetString("email", usr.Email);
                        HttpContext.Session.SetString("rol", usr.Role.ToString());
                        sonuc = new { hata = false, mesaj = "Kullanıcı Başarı ile oluşturuldu. ", res = "" + "/" };
                    }
                    else
                    {
                        HttpContext.Session.SetString("email", user.Email);
                        HttpContext.Session.SetString("emailkodu", usr.EmailConfirmed);
                        sonuc = new { hata = true, mesaj = "Bu mail daha önce kullanılmış", res = "" + "/Login/MailHata" };
                    }

                }
                else
                {
                    sonuc = new { hata = true, mesaj = " Kayıt oluştururken hata oluştu", res = "" + "/" };

                }


            }
            catch (Exception e)
            {

            }

            return Json(sonuc);

        }


		[HttpPost]
		public Task<JsonResult> seeAll()
		{
			var sonuc = new { hata = true, mesaj = "Error", res = "" };
			try
			{

				HttpContext.Session.SetInt32("kategoriid",0);
				HttpContext.Session.SetString("tedarikciadi","");


			    sonuc = new { hata = false, mesaj = "Error", res = "/Tedarikci/List" };
				return Task.FromResult(Json(sonuc));
			}
			catch (Exception)
			{


				return Task.FromResult(Json(sonuc));
			}
		}

        [HttpGet]
        public Task<JsonResult> getMenuLayer()
        {
            //if (start < 1) { start = 1; }
            //start = start - 1;
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _websiteService.getMenuLayer1(lang);
            return Task.FromResult(Json(sonuc));
        }

    }
}