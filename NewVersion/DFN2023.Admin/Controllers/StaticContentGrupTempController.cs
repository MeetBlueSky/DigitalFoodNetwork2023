using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DFN2023.Admin.Models;
using DFN2023.Contracts;
using DFN2023.Entities.EF;
using DFN2023.Entities.Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DFN2023.Admin.Controllers
{
    public class StaticContentGrupTempController : BaseController
    {
        private readonly IAdminService _adminService;
        public StaticContentGrupTempController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "Static Content Grup Template";
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
        public Task<JsonResult> getStaticContentGrupTemp([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getStaticContentGrupTemp(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }


        [HttpPost]
        public Task<JsonResult> CreatedStaticContentGrupTemp(StaticContentGrupTemp pro)
        {

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (pro != null)
                {
                    var result = _adminService.createStaticContentGrupTemp(pro);
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
        public Task<JsonResult> DeleteStaticContentGrupTemp(StaticContentGrupTemp pro)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (pro != null)
                {
                    var result = _adminService.deleteStaticContentGrupTemp(pro);
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
