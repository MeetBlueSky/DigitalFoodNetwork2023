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
            PublicModel pm = new PublicModel(); if (usr != null)
            {
                //var m= _websiteService.getMesajList(usr.Id, false);
                pm.tedarikcirol = EnumRol.Tedarikci;
                pm.user = usr;

            return View(pm);
        }
        
            else
            {
                return RedirectToAction("Index", "Home");
            }
         }
        [HttpPost]
        public Task<JsonResult> getMesajList(int start, int finish)
        {
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            if (usr != null)
            {
                var sonuc = _websiteService.getMesajList(usr.Id, start, finish, usr.Role,EnumRol.Tedarikci);
                return Task.FromResult(Json(sonuc));
            }
            else
            {
                return Task.FromResult(Json(null));
            }
        }
        public IActionResult Detay(int id)
        {
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            if (usr!=null)
            {
                HttpContext.Session.SetInt32("mesaydetayid", id);
                pm.user = usr;
                pm.selectcompanyid = id;
                pm.mesajdetay = _websiteService.getMesajDetay(usr.Id, id, usr.Role,0,4);
                pm.tedarikciadi = _websiteService.sirketOzelligi(id, usr.Role);
                return View(pm);
            }
        
            else
            {
                return RedirectToAction("Index", "Home");
            }
    
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
                    mesaj.MessageContent = mesajtext;
                    mesaj.CreateDate = DateTime.Now;
                    mesaj.LastIP = HttpContext.Connection.RemoteIpAddress?.ToString();
                    mesaj.Status = 1; 
                    mesaj.IsShow = false;

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

        [HttpPost]
        public Task<JsonResult> getMesajDetay(int start,int finish)
        {
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();
            if (usr != null)
            {
                int mid = Convert.ToInt32(HttpContext.Session.GetInt32("mesaydetayid"));
                var sonuc = _websiteService.getMesajDetay(usr.Id, mid, usr.Role, start, finish);
                return Task.FromResult(Json(sonuc));
            }
            else
            {
                return Task.FromResult(Json(null));
            }
            }


    }
    }

