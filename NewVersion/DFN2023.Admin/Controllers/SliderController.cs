using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using DFN2023.Admin.Models;
using DFN2023.Business.Validators;
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
    public class SliderController : BaseController
    {
        private readonly IAdminService _adminService;
        public SliderController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }
        public IActionResult Index()
        {
            ViewData["page"] = "Slider";

            List<SelectListItem> target = new List<SelectListItem>();
            target.Add(new SelectListItem { Text = "Yeni bir pencerede aç", Value = "_blank" });
            target.Add(new SelectListItem { Text = "Bağlantıyı aynı pencerede aç", Value = "_self" });
            target.Add(new SelectListItem { Text = "Bağlantıyı bu pencerede aç", Value = "_parent" });
            target.Add(new SelectListItem { Text = "Bağlantıyı bu pencerede aç", Value = "_top" });
            target.Add(new SelectListItem { Text = "Bağlantıyı belirtilmiş pencerede aç", Value = "framename" });
            ViewBag.target = target;

            List<SelectListItem> showLocation = new List<SelectListItem>();
            target.Add(new SelectListItem { Text = "Anasayfa", Value = "1" });
            ViewBag.ShowLocation = showLocation;

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var anadizin = config["AppSettings:anaDizin"].ToString();

            ViewData["website"] = anadizin;
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            WepPageModel spm = new WepPageModel();
            spm.lang = lang;
            spm.language = CultureInfo.CurrentCulture.Name.ToLower();
            return View(spm);

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
                        var fileName = "homeslider\\" + yeniisim;
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
        public Task<JsonResult> getSlider([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getSlider(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

      

        [HttpPost]
        public Task<JsonResult> createSlider(Slider selectedSlider)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            SliderValidator vn = new SliderValidator();
            selectedSlider.LangId = lang;
            ValidationResult result = vn.Validate(selectedSlider);

            if (result.Errors.Count == 0)
            {
                selectedSlider.LangId = lang;
                selectedSlider.LastIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                var sonuc = _adminService.createSlider(selectedSlider);
                return Task.FromResult(Json("true"));
            }
            else
            {
                var s = "";
                foreach (var item in result.Errors)
                { 
                    s += item.ErrorMessage.ToString() + "</br> ";

                }
                return Task.FromResult(Json(s));
            }
        }





        [HttpPost]
        public Task<JsonResult> deleteSlider(Slider selectedSlider)
        {
            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (selectedSlider != null)
                {
                    var result = _adminService.deleteSlider(selectedSlider);
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
