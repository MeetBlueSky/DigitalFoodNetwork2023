using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
//using Admin.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DFN2023.Admin.Models;
using DFN2023.Admin.Validators;
using DFN2023.Contracts;
using DFN2023.Entities.EF;
using DFN2023.Entities.Models;

namespace DFN2023.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IAdminService _adminService;
        public ProductController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult ProductBase()
        {
            ViewData["page"] = "Kategori";
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var anadizin = config["AppSettings:anaDizin"].ToString();//

            ViewData["website"] = anadizin;
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            WepPageModel spm = new WepPageModel();
            spm.lang = lang;
            spm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(spm);
        }

        [HttpPost]
        public Task<JsonResult> getProductBase([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);

            var sonuc = _adminService.getProductBase(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]

        public Task<JsonResult> CreatedProductBase(ProductBase ct)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            //ct.LangId = lang;

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (ct != null)
                {

                    var result = _adminService.createProductBase(ct);
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
        public Task<JsonResult> DeleteProductBase(ProductBase ct)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (ct != null)
                {
                    var result = _adminService.deleteProductBase(ct);
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


        public IActionResult ProductCompany()
        {
            ViewData["page"] = "Kategori";
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var anadizin = config["AppSettings:anaDizin"].ToString();//

            ViewData["website"] = anadizin;
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            WepPageModel spm = new WepPageModel();
            spm.lang = lang;
            spm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(spm);
        }

        [HttpPost]
        public Task<JsonResult> getProductCompany([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);

            var sonuc = _adminService.getProductCompany(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]

        public Task<JsonResult> CreatedProductCompany(ProductCompany ct)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            //ct.LangId = lang;

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (ct != null)
                {

                    var result = _adminService.createProductCompany(ct);
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
        public Task<JsonResult> DeleteProductCompany(ProductCompany ct)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (ct != null)
                {
                    var result = _adminService.deleteProductCompany(ct);
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }


}
