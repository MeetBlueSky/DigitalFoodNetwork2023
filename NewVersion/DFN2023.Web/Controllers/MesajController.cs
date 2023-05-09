using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using DFN2023.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFN2023.Web.Controllers
{
    public class MesajController : BaseController
    {
        private readonly IWebsiteService _websiteService;
        public MesajController(IWebsiteService parametreService)
        {
            _websiteService = parametreService;
        }
        public IActionResult List()
        {
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            pm.user = usr;
            pm.mesajlist= _websiteService.getMesajList(usr.Id,usr.Role);

            return View(pm);
        }
        public IActionResult Detay(int id)
        {
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            pm.user = usr;
            pm.selectcompanyid = id;
            pm.mesajdetay = _websiteService.getMesajDetay(usr.Id,id,usr.Role);

            return View(pm);
        }
        [HttpPost]
        public Task<JsonResult> mesajYazUser(string mesajtext,int touser)
        {
            try
            {
                var sonuc = new { hata = true, mesaj = "Error", res = "" };
                var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
                if (mesajtext.Length > 0 && usr.Id > 0)

                {
                    Message mesaj = new Message();
                    mesaj.FromUser = usr.Id;
                    mesaj.ToUser= touser;
                    mesaj.FromRolId = 3;
                    mesaj.MessageContent = mesajtext;
                    mesaj.CreateDate = DateTime.Now;
                    mesaj.LastIP = 1;
                    mesaj.Status = 1;
                    mesaj.CompanyShow = false;
                    mesaj.UserShow = false;
                    var dgr = _websiteService.mesajYazUser(mesaj);
                    if (dgr)
                    {
                        
                            sonuc = new { hata = false, mesaj = "Mesaj gönderildi", res = "" };

                       
                    }
                    else
                    {
                        sonuc = new { hata = true, mesaj = "Hata Oluştu", res = "" };
                    }
                }
                else
                {

                    sonuc = new { hata = true, mesaj = "Giriş Yapın", res = "" };
                }

                return Task.FromResult(Json(sonuc));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
