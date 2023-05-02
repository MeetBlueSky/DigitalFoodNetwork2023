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
using System.Net.Http.Headers;
using DFN2023.Admin.Helpers;

namespace DFN2023.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IAdminService _adminService;
        public CompanyController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult Company()
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
        public Task<JsonResult> getCompany([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);

            var sonuc = _adminService.getCompany(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]

        public Task<JsonResult> CreatedCompany(Company ct)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            //ct.LangId = lang;

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (ct != null)
                {

                    var result = _adminService.createCompany(ct);
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
        public Task<JsonResult> DeleteCompany(Company ct)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (ct != null)
                {
                    var result = _adminService.deleteCompany(ct);
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


        public IActionResult CompanyType()
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
        public Task<JsonResult> getCompanyType([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);

            var sonuc = _adminService.getCompanyType(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]

        public Task<JsonResult> CreatedCompanyType(CompanyType ct)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            //ct.LangId = lang;

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {
                // HttpContext.Session.GetObjectFromJson("AktifKullanici");

                if (ct != null)
                {

                    var result = _adminService.createCompanyType(ct);
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
        public Task<JsonResult> DeleteCompanyType(CompanyType ct)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (ct != null)
                {
                    var result = _adminService.deleteCompanyType(ct);
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
        public async Task<JsonResult> UploderImage(IList<IFormFile> files)
        {
            try
            {

                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var webRootPath = config["AppSettings:urunResimPath"].ToString();
                long maxImageSize = Convert.ToInt64(config["AppSettings:ImageUploadMaxStaticContent"]) * (1000);

                var filelist = files;
                var yeniisim = Guid.NewGuid().ToString();

                if (filelist.Count > 0)
                {
                    foreach (var item in filelist)
                    {
                        if (item.Length <= maxImageSize)
                        {
                            string[] sn = item.FileName.Split('.');
                            yeniisim = yeniisim + "." + sn[sn.Length - 1];
                            var fileName = "static\\" + yeniisim;
                            var fileContent = ContentDispositionHeaderValue.Parse(item.ContentDisposition);
                            using (var fileStream = new FileStream(webRootPath + "\\" + fileName, FileMode.Create))
                            {
                                await item.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            return Json("false");
                        }
                    }
                }
                return Json(yeniisim);
            }
            catch (Exception)
            {

                return Json("false");
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }


}
