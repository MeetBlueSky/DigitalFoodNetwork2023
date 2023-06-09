﻿using System;
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
using DFN2023.Entities.DTO;
using FluentValidation;

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
            CompanyPageModel cpm = new CompanyPageModel();
            cpm.CompanyType = _adminService.getCompanyTypeList(lang);
            cpm.OfficialCountry = cpm.MapCountry = _adminService.getCountryList(lang);
            cpm.OfficialCity = cpm.MapCity = _adminService.getCityList(lang);
            cpm.OfficialCounty = cpm.MapCounty = _adminService.getCountyList(lang);
            cpm.lang = lang;
            cpm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(cpm);
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
            CompanyValidator vn = new CompanyValidator();
            ValidationResult result = vn.Validate(ct);

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {
                if (result.Errors.Count == 0)
                {
                    ct.LastIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    var result2 = _adminService.createCompany(ct);
                    if (result2 != null)
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
                    var s = "";
                    foreach (var item in result.Errors)
                    {
                        s += item.ErrorMessage.ToString() + "</br> ";

                    }
                    //return Task.FromResult(Json(s));
                    sonuc = new { hata = true, mesaj = s, res = "" };
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
            User usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");

            int lang = getLang(CultureInfo.CurrentCulture.Name);
            //ct.LangId = lang;
            CompanyTypeValidator vn = new CompanyTypeValidator();
            ValidationResult result = vn.Validate(ct);

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (result.Errors.Count == 0)
                {


                    var result2 = _adminService.createCompanyType(ct);
                    if (result2 != null)
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
                    var s = "";
                    foreach (var item in result.Errors)
                    {
                        s += item.ErrorMessage.ToString() + "</br> ";

                    }
                    //return Task.FromResult(Json(s));
                    sonuc = new { hata = true, mesaj = s, res = "" };
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

        
        [HttpPost]
        public Task<JsonResult> getCompanyImages([FromBody] DtParameters pi)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getCompanyImage(pi);

            return Task.FromResult(Json(sonuc));

        }

        [HttpPost]
        public Task<JsonResult> CreatedCompanyImage(CompanyImageDTO img)
        {



            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (img != null)
                {
                    var result = _adminService.createCompanyImage(img);
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
        public Task<JsonResult> DeleteCompanyImage(CompanyImageDTO img)
        {



            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (img != null)
                {
                    var result = _adminService.deleteCompanyImage(img);
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
