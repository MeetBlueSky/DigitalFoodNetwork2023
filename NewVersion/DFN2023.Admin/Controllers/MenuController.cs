using DFN2023.Admin.Models;
using DFN2023.Admin.Validators;
using DFN2023.Contracts;
using DFN2023.Entities.EF;
using DFN2023.Entities.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

namespace DFN2023.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IAdminService _adminService;
        public MenuController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "Menü Yönetim Sayfası";
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var anadizin = config["AppSettings:anaDizin"].ToString();//
            MenuPageModel mpm = new();
            mpm.ParentList = _adminService.getMenuParentList();
            mpm.ChildList = _adminService.getMenuChildList();
            ViewData["website"] = anadizin;

            return View(mpm);
        }
        [HttpPost]
        public Task<JsonResult> getMenuManagement([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getMenuManagement(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }
        
        public Task<JsonResult> CreatedMenu(MenuManagement menu)
        {
            MenuValidators vn = new();
            ValidationResult result = vn.Validate(menu);

            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            try
            {

                if (menu != null)
                {
                    menu.LangId = lang;
                    if (menu.Status == 0)
                    {
                        menu.Status = 2;
                    }

                    if (result.Errors.Count == 0)
                    {
                        _adminService.createMenuManagement(menu);
                        sonuc = new { hata = false, mesaj = "Kayıt İşlemi Başarılı", res = "" };
                        return Task.FromResult(Json(sonuc));
                    }
                    else
                    {
                        var s = "";
                        foreach (var item in result.Errors)
                        {
                            s += item.ErrorMessage.ToString() + "</br> ";
                        }
                        sonuc = new { hata = true, mesaj = s, res = "" };
                    }


                }

            }
            catch (Exception e)
            {
                sonuc = new { hata = true, mesaj = "Error", res = "" };
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
                        var fileName = "menu\\" + yeniisim;
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
        public Task<JsonResult> DeleteMenu(int id)
        {


            var sonuc = new { hata = true, mesaj = "Error", res = "" };
            try
            {

                if (id != 0)
                {
                    var result = _adminService.deleteMenuManagement(id);
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

