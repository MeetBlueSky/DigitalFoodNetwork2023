using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DFN2023.Entities.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using DFN2023.Admin.Helpers;
using DFN2023.Admin.Models;
using DFN2023.Admin.Validators;
using DFN2023.Contracts;
using DFN2023.Entities.EF;

namespace DFN2023.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAdminService _adminService;
        public UserController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }
        public IActionResult Index()
        {
            ViewData["page"] = "User";
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var anadizin = config["AppSettings:anaDizin"].ToString();//T/Pz5579NLmzyTIhq/VlCw==    123

            ViewData["website"] = anadizin;
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            WepPageModel spm = new WepPageModel();
            spm.lang = lang;
            spm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(spm);
        }
        [HttpPost]
        public Task<JsonResult> getUser([FromBody] DtParameters dtParameters)
        {
            var sonuc = _adminService.getUsers(dtParameters);
            return Task.FromResult(Json(sonuc));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public Task<JsonResult> CreatedUser(User usr)
        {

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (usr != null)
                {
                    if (usr.Id > 0 && usr.Password != null && usr.Password.Length > 0)
                    {
                        usr.Password = XamarinUtils.Sifrele(usr.Password);
                    }

                    if (usr.Id ==0 && usr.Password != null && usr.Password.Length > 0)
                    {
                        usr.Password = XamarinUtils.Sifrele(usr.Password);
                    }



                    var result = _adminService.createUser(usr);
                    if (result != null)
                    {

                        sonuc = new { hata = false, mesaj = "Başarılı", res = "" };
                    }
                    else
                    {
                        sonuc = new { hata = true, mesaj = "Hata", res = "" };

                    }

                }
                else
                {
                    sonuc = new { hata = true, mesaj = "Hata", res = "" };

                }


            }
            catch (Exception e)
            {
                sonuc = new { hata = true, mesaj = "Error", res = "" };
            }

            return Task.FromResult(Json(sonuc));

        }



        [HttpPost]
        public Task<JsonResult> DeleteUser(User usr)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (usr != null)
                {
                    var result = _adminService.deleteUser(usr);
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
    }
}
