using DFN2023.Admin.Models;
using DFN2023.Admin.Validators;
using DFN2023.Contracts;
using DFN2023.Entities.EF;
using DFN2023.Entities.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace DFN2023.Admin.Controllers
{
    public class SliderHeaderController : BaseController
    {
        private readonly IAdminService _adminService;
        public SliderHeaderController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }
        public IActionResult Index()
        {
            ViewData["page"] = "Slider Header";

            //List<SelectListItem> target = new List<SelectListItem>();
            //target.Add(new SelectListItem { Text = "Yeni bir pencerede aç", Value = "_blank" });
            //target.Add(new SelectListItem { Text = "Bağlantıyı aynı pencerede aç", Value = "_self" });
            //target.Add(new SelectListItem { Text = "Bağlantıyı bu pencerede aç", Value = "_parent" });
            //target.Add(new SelectListItem { Text = "Bağlantıyı bu pencerede aç", Value = "_top" });
            //target.Add(new SelectListItem { Text = "Bağlantıyı belirtilmiş pencerede aç", Value = "framename" });
            //ViewBag.target = target;

            //List<SelectListItem> showLocation = new List<SelectListItem>();
            //target.Add(new SelectListItem { Text = "Anasayfa", Value = "1" });
            //ViewBag.ShowLocation = showLocation;

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
        public Task<JsonResult> getSliderHeader([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getSliderHeader(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }



        [HttpPost]
        public Task<JsonResult> createSliderHeader(SliderHeader selectedSliderHeader)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            SliderHeaderValidator vn = new SliderHeaderValidator();
            //selectedSliderHeader.LangId = lang;
            ValidationResult result = vn.Validate(selectedSliderHeader);

            if (result.Errors.Count == 0)
            {
                selectedSliderHeader.CreatedBy = 1;
                selectedSliderHeader.CreateDate = DateTime.Now;
                var sonuc = _adminService.createSliderHeader(selectedSliderHeader);
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
        public Task<JsonResult> deleteSliderHeader(SliderHeader selectedSliderHeader)
        {
            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (selectedSliderHeader != null)
                {
                    var result = _adminService.deleteSliderHeader(selectedSliderHeader);
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

