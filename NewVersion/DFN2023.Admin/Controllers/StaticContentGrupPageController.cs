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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace DFN2023.Admin.Controllers
{
    public class StaticContentGrupPageController : BaseController
    {
        private readonly ILogger<StaticContentGrupPageController> _logger;
       
            
        
        private readonly IAdminService _adminService;
        public StaticContentGrupPageController(IAdminService parametreService,  ILogger<StaticContentGrupPageController> logger)
        {
            _adminService = parametreService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "Static Content Page";

            var getStaticContentGrupTempList = _adminService.listStaticContentGrupTemp();
            SelectList listStaticContentGrupTemp = new SelectList(getStaticContentGrupTempList, "Id", "Name");
            ViewBag.staticContentGrupTemp = listStaticContentGrupTemp;

            ViewBag.ImageSelect = ViewBag.VideoSelect = new List<SelectListItem> {
                new SelectListItem { Text = "...", Value = "-1" },
                new SelectListItem { Text = "Yok", Value = "0" },
                new SelectListItem { Text = "Var", Value = "1" },
            };

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
            ViewData["sunucuEntityStaticFolder"] = config["AppSettings:sunucuEntityStaticGroupFolder"].ToString();

            int lang = getLang(CultureInfo.CurrentCulture.Name);
            WepPageModel spm = new WepPageModel();
            spm.lang = lang;
            spm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(spm);
        }

        [HttpPost]
        public Task<JsonResult> getStaticContentGrupPage([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getStaticContentGrupPage(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }


        [HttpPost]
        public Task<JsonResult> CreatedStaticContentGrupPage(StaticContentGrupPage pro)
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
                    var result = _adminService.createStaticContentGrupPage(pro);
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
        public Task<JsonResult> DeleteStaticContentGrupPage(StaticContentGrupPage pro)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (pro != null)
                {
                    var result = _adminService.deleteStaticContentGrupPage(pro);
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



        IActionResult ReportError(string msg)
        {
            Response.ContentType = "text/plain";
            Response.StatusCode = 500;
            return Content("ERROR:" + msg);
        }

        async Task<byte[]> FullReadDataAsync()
        {
            byte[] data = new byte[(int)Request.ContentLength];
            int len = 0;
            while (len < data.Length)
            {
                int rc = await Request.Body.ReadAsync(data, len, data.Length - len);
                if (rc == 0)
                    throw new Exception("Unexpected request data");
                len += rc;
            }
            return data;
        }

        public async Task<IActionResult> ImageUploadHandler(string type, string name)
        {
            if (Request.ContentLength > 4000000)
            {
                return ReportError("file too big");
            }

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var webRootPath = config["AppSettings:urunResimPath"].ToString();
            string ext = Path.GetExtension(name).ToLower();

            if (type.StartsWith("image/"))
            {
                switch (ext)
                {
                    case ".jpeg":
                    case ".jpg":
                    case ".png":
                        break;
                    default:
                        return ReportError("invalid file extension.");
                }

                byte[] data = await FullReadDataAsync();

                string filename = "/static/" + DateTime.Now.Ticks + "-" + Guid.NewGuid() + ext;

                string fullpath = Path.Combine(webRootPath, filename.TrimStart('/'));
                string fulldir = Path.GetDirectoryName(fullpath);
                if (!Directory.Exists(fulldir)) Directory.CreateDirectory(fulldir);

                System.IO.File.WriteAllBytes(fullpath, data);

                return Content("READY:" + filename);

            }
            else
            {
                switch (ext)
                {
                    case ".zip":
                    case ".rar":
                    case ".pdf":
                    case ".doc":
                    case ".docx":
                    case ".xls":
                    case ".xlsx":
                    case ".rtf":
                    case ".txt":
                        break;
                    default:
                        return ReportError("Invalid file extension");
                }

                string filename = "/static/" + DateTime.Now.Ticks + "-" + Guid.NewGuid() + ext;

                byte[] data = await FullReadDataAsync();

                string fullpath = Path.Combine(webRootPath, filename.TrimStart('/'));
                string fulldir = Path.GetDirectoryName(fullpath);
                if (!Directory.Exists(fulldir)) Directory.CreateDirectory(fulldir);

                System.IO.File.WriteAllBytes(fullpath, data);

                return Content("READY:" + filename);


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
