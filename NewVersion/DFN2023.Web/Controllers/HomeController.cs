using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using DFN2023.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DFN2023.Web.Controllers
{
    public class HomeController : Controller
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
                        sonuc = new { hata = true, mesaj = "Bu kullanıcı adı veya mail daha önce kullanılmış", res = "tr" + "/" };
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
      

    }
}