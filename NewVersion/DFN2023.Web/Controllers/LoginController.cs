using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DFN2023.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IWebsiteService _websiteService;
        public LoginController(IWebsiteService parametreService)
        {
            _websiteService = parametreService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GirisYap(UserDTO user)
        {

            var sonuc = new { hata = true, mesaj = "Teknik Ariza", res = "/" };
            try
            {
                if (user.UserName != null && user.Password != null)
                {

                    var usr = _websiteService.CheckUser(user.UserName, user.Password);

                    if (usr.Count > 0)
                    {
                        if (usr[0].Role== EnumRol.Tuketici || usr[0].Role == EnumRol.Tedarikci)
                        {
                            if (user.Password.Equals(XamarinUtils.SifreCoz(usr[0].Password)))
                            {
                                user.Password = "";
                                HttpContext.Session.SetObjectAsJson("AktifKullanici", usr[0]);
                                TempData["adsoyad"] = usr[0].Name + " " + usr[0].Surname;
                                TempData["username"] = usr[0].UserName;
                                TempData["rol"] = usr[0].Role;


                                sonuc = new { hata = false, mesaj = "Giriş İşleminiz Başarılı. Lütfen Bekleyiniz", res = "tr" + "/Home/Index" };
                            }
                            else
                            {
                                sonuc = new { hata = true, mesaj = "Yanlış Şifre", res = "tr" + "/" };
                                TempData["girisdurum"] = "Şifrenizi Yanlış Girdiniz";

                                HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                            }

                        }
                        else
                        {

                            sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res = "tr" + "/" };
                        }
                    }
                    else
                    {
                        sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res = "tr" + "/" };
                        TempData["girisdurum"] = "Böyle bir kullanıcı Bulunamadı";

                        HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                    }


                }
                else
                {
                    sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res = "tr" + "/" };
                    TempData["girisdurum"] = "Böyle bir kullanıcı Bulunamadı";

                    HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                }

            }
            catch (Exception e)
            {

            }

            return Json(sonuc);

        }
        [HttpPost]
        public Task<JsonResult> SignIn(User user)
        {
            var sonuc = new { hata = true, mesaj = "Error", res = "" };
           // int lang = getLang(CultureInfo.CurrentCulture.Name);
            user.Role = 2;
            user.CreateDate = DateTime.Now;
            user.Status = 1;
            //user.langId = lang;
            user.LastIP = "";
            //var result = _websiteService.SignIn(user);
            //if (result != null)
            //{
            //    sonuc = new { hata = false, mesaj = "Kayıt İşlemi Başarılı", res = "" };
            //}
            //else
            //{
            //    sonuc = new { hata = true, mesaj = "Kayıt İşlemi Başarısız", res = "" };
            //}
            return Task.FromResult(Json(sonuc));
        }
        [HttpPost]
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        private IActionResult RedirectToLocal(string returnUrl) => RedirectToAction(nameof(HomeController.Index), "Oturum");
    }
}
