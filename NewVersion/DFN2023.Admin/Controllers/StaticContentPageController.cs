using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

using DFN2023.Admin.Helpers;
using DFN2023.Admin.Models;
using DFN2023.Contracts;
using DFN2023.Entities.EF;
using DFN2023.Entities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DFN2023.Admin.Controllers
{
    public class StaticContentPageController : BaseController
    {
        private readonly IAdminService _adminService;
        public StaticContentPageController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        [HttpGet]
        [Route("{culture}/StaticContentPage/Index")]
        [Route("{culture}/StaticContentPage/Index/{id?}")]
        public IActionResult Index()
        {
            ViewData["GrupPageId"] = 0;

            ViewData["page"] = "Static Content Page";
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var getStaticContentGrupPageList = _adminService.listStaticContentGrupPage(lang).OrderBy(p => p.OrderNo).ToList();
            SelectList listStaticContentGrupPage = new SelectList(getStaticContentGrupPageList, "Id", "Title");
            ViewBag.staticContentGrupPage = listStaticContentGrupPage;

            ViewBag.ImageSelect = ViewBag.VideoSelect = ViewBag.AttachmentSelect = new List<SelectListItem> {
                new SelectListItem { Text = "...", Value = "-1" },
                new SelectListItem { Text = "Yok", Value = "0" },
                new SelectListItem { Text = "Var", Value = "1" },
            };

            var getStaticContentTempList = _adminService.listStaticContentTemp();
            SelectList listStaticContentTemp = new SelectList(getStaticContentTempList, "Id", "Name");
            ViewBag.staticContentTemp = listStaticContentTemp;

            ViewBag.StatusSelect = new List<SelectListItem> {
                new SelectListItem { Text = "...", Value = "-1" },
                new SelectListItem { Text = "Pasif", Value = "0" },
                new SelectListItem { Text = "Aktif", Value = "1" },
            };

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ViewData["uploadedDocMaxSize"] = Convert.ToInt32(config["AppSettings:DocUploadMaxSize"]);
            ViewData["website"] = config["AppSettings:anaDizin"].ToString();
            ViewData["rootFolder"] = config["AppSettings:rootFolder"].ToString();
            ViewData["staticContentFolder"] = config["AppSettings:staticContentFolder"].ToString();
            ViewData["sunucuEntityStaticFolder"] = config["AppSettings:sunucuEntityStaticFolder"].ToString();

            WepPageModel spm = new WepPageModel();
            spm.lang = lang;
            spm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(spm);
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            ViewData["GrupPageId"] = id;

            ViewData["page"] = "Static Content Page";
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var getStaticContentGrupPageList = _adminService.listStaticContentGrupPage(lang).OrderBy(p => p.OrderNo).ToList();
            SelectList listStaticContentGrupPage = new SelectList(getStaticContentGrupPageList, "Id", "Title");
            ViewBag.staticContentGrupPage = listStaticContentGrupPage;

            var getStaticContentTempList = _adminService.listStaticContentTemp();
            SelectList listStaticContentTemp = new SelectList(getStaticContentTempList, "Id", "Name");
            ViewBag.staticContentTemp = listStaticContentTemp;

            ViewBag.StatusSelect = new List<SelectListItem> {
                new SelectListItem { Text = "...", Value = "-1" },
                new SelectListItem { Text = "Pasif", Value = "0" },
                new SelectListItem { Text = "Aktif", Value = "1" },
            };

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ViewData["uploadedDocMaxSize"] = Convert.ToInt32(config["AppSettings:DocUploadMaxSize"]);
            ViewData["website"] = config["AppSettings:anaDizin"].ToString();
            ViewData["rootFolder"] = config["AppSettings:rootFolder"].ToString();
            ViewData["staticContentFolder"] = config["AppSettings:staticContentFolder"].ToString();
            ViewData["sunucuEntityStaticFolder"] = config["AppSettings:sunucuEntityStaticFolder"].ToString();

            WepPageModel spm = new WepPageModel();
            spm.lang = lang;
            spm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(spm);
        }

        [HttpPost]
        public Task<JsonResult> getStaticContentPage([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getStaticContentPage(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }


        [HttpPost]
        public Task<JsonResult> CreatedStaticContentPage(StaticContentPage pro)
        {
            string ln = CultureInfo.CurrentCulture.Name;
            int lang = getLang(ln);
            pro.LangId = lang;
            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (pro != null)
                {
                    if (string.IsNullOrEmpty(pro.Link))
                        pro.Link = ln.ToLower() + "/static/Grup/" + UrlCleaner.GetSanitizedTextForURL(pro.Title) + "/";
                    var result = _adminService.createStaticContentPage(pro);
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
        public Task<JsonResult> DeleteStaticContentPage(StaticContentPage pro)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (pro != null)
                {
                    var result = _adminService.deleteStaticContentPage(pro);
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

                var filelist = files;
                var yeniisim = Guid.NewGuid().ToString();

                if (filelist.Count > 0)
                {
                    foreach (var item in filelist)
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
                }
                return Json(yeniisim);
            }
            catch (Exception)
            {

                return Json("false");
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploderImage2(IList<IFormFile> files)
        {
            try
            {

                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var webRootPath = config["AppSettings:urunResimPath"].ToString();
                var staticFolderPath = config["AppSettings:staticContentFolder"].ToString();


                var filelist = files;
                var yeniisim = Guid.NewGuid().ToString();

                if (filelist.Count > 0)
                {
                    foreach (var item in filelist)
                    {
                        string[] sn = item.FileName.Split('.');
                        yeniisim = yeniisim + "." + sn[sn.Length - 1];
                        var fileName = staticFolderPath + "\\" + yeniisim;
                        var fileContent = ContentDispositionHeaderValue.Parse(item.ContentDisposition);
                        //using (var fileStream = new FileStream(webRootPath + "\\" + fileName, FileMode.Create))
                        //{
                        //    await item.CopyToAsync(fileStream);
                        //}
                        using (var fileStream = new FileStream("\\assets\\imageupload\\" + fileName, FileMode.Create))
                        {
                            await item.CopyToAsync(fileStream);
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
        public async Task<JsonResult> DocUploadImage()
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var webRootPath = config["AppSettings:urunResimPath"].ToString();
                var anaDizin = config["AppSettings:anaDizin"].ToString();
                var CKEditorFuncNum = Request;
                 
                var urls = new List<string>();

                //If folder of new key is not exist, create the folder.
                if (!Directory.Exists(webRootPath)) Directory.CreateDirectory(webRootPath);

                foreach (var contentFile in Request.Form.Files)
                {
                    if (contentFile != null && contentFile.Length > 0)
                    {
                        await contentFile.CopyToAsync(new FileStream($"{webRootPath}\\static\\{contentFile.FileName}", FileMode.Create));
                        urls.Add($"{anaDizin}static/{contentFile.FileName}");
                    }
                }

                return Json(urls);
            }
            catch (Exception e)
            {
                return Json(new { error = new { message = e.Message } });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploderDokuman(IList<IFormFile> file)
        {
            try
            {

                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var webRootPath = config["AppSettings:urunResimPath"].ToString();
                var staticFolderPath = config["AppSettings:staticContentFolder"].ToString();
                //var webRootPath = "C:\\inetpub\\wwwroot\\images\\";

                var filelist = file;
                var yeniisim = ""; Guid.NewGuid().ToString();

                if (filelist.Count > 0)
                {
                    foreach (var item in filelist)
                    {
                        //string[] sn = item.FileName.Split('.');

                        string fileExtension = Path.GetExtension(item.FileName);
                        string fileNamePure = Path.GetFileNameWithoutExtension(item.FileName);
                        fileNamePure = Helpers.UrlCleaner.SanitizeURL(fileNamePure);
                        //item.FileName = fileNamePure + fileExtension;

                        //yeniisim = yeniisim + item.FileName;
                        yeniisim = yeniisim + fileNamePure + fileExtension;
                        var fileName = staticFolderPath + "\\" + yeniisim;
                        var fileContent = ContentDispositionHeaderValue.Parse(item.ContentDisposition);
                        using (var fileStream = new FileStream(webRootPath + "\\" + fileName, FileMode.Create))
                        {
                            await item.CopyToAsync(fileStream);
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

        public Task<JsonResult> getStaticContentImagesForGallery()
        {
            var sonuc = new { hata = true, mesaj = "Error", res = "", list = new List<string>() };    
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var webRootPath = config["AppSettings:urunResimPath"].ToString();
                var staticContentFolder = config["AppSettings:staticContentFolder"].ToString();

                string[] files = Directory.GetFiles(webRootPath + "//" + staticContentFolder);
                List<string> fileNames = new List<string>();

                if (files.Length > 0)
                {
                    foreach (var item in files)
                    {
                        //string[] sn = item.FileName.Split('.');
                        fileNames.Add(Path.GetFileName(item));

                    }
                }

                sonuc = new { hata = false, mesaj = "Başarılı bir şekilde sıralama değişmiştir", res = "", list = fileNames };
              
                return Task.FromResult(Json(sonuc));
            }
            catch (Exception)
            {

                return Task.FromResult(Json(sonuc));
            }

        }

    }
}
