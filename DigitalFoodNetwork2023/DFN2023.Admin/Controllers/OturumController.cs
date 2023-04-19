using DFN2023.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using DFN2023.Admin.Helpers;
using DFN2023.Contracts;
using DFN2023.Entities.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace DFN2023.Admin.Controllers
{
    public class OturumController : BaseController
    {
        private readonly IAdminService _adminService;
        public OturumController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }
        public IActionResult Login()
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

                    var usr = _adminService.CheckUser(user.UserName, user.Password);

                    if (usr.Count >0)
                    {
                        if (user.Password.Equals(XamarinUtils.SifreCoz(usr[0].Password)))
                        {
                            user.Password = "";
                            HttpContext.Session.SetObjectAsJson("AktifKullanici", usr[0]);
                            TempData["adsoyad"] = usr[0].Name + " " + usr[0].Surname;
                            TempData["username"] = usr[0].UserName;
                            TempData["rol"] = usr[0].Role;

                            //string defaultCulture = _adminService.setDefaultLanguage();
                            //System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(defaultCulture);
                            //HttpContext.Session.SetString("culture", defaultCulture);

                            //LoginDTO ldto = new LoginDTO
                            //{
                            //    UserId = usr[0].Id,
                            //    IP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                            //    Date = DateTime.Now,
                            //};
                            //_adminService.createLogin(ldto);

                            sonuc = new { hata = false, mesaj = "Giriş İşleminiz Başarılı. Lütfen Bekleyiniz", res = user.Lang + "/Home/Index" };
                        }
                        else
                        {
                            sonuc = new { hata = true, mesaj = "Yanlış Şifre", res = user.Lang + "/" };
                            TempData["girisdurum"] = "Şifrenizi Yanlış Girdiniz";

                            HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                        }

                    }
                    else
                    {
                        sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res = user.Lang+"/" };
                        TempData["girisdurum"] = "Böyle bir kullanıcı Bulunamadı";

                        HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                    }


                }
                else
                {
                    sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res = user.Lang + "/" };
                    TempData["girisdurum"] = "Böyle bir kullanıcı Bulunamadı";

                    HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                }

            }
            catch (Exception e)
            {

            }

            return Json(sonuc);

        }
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        private IActionResult RedirectToLocal(string returnUrl) => RedirectToAction(nameof(HomeController.Index), "Oturum");


    }
}

