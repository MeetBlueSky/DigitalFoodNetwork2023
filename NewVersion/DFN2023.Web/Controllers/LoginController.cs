﻿using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using DFN2023.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Mail;

namespace DFN2023.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IWebsiteService _websiteService;
        public LoginController(IWebsiteService parametreService)
        {
            _websiteService = parametreService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GirisYap(UserDTO user)
        {

            var sonuc = new { hata = true, mesaj = "Teknik Ariza", res = "/" };
            try
            {
                if (user.Email != null && user.Password != null)
                {

                    var usr = _websiteService.CheckUser(user.Email, user.Password);
                    if (usr.Id==-1)
                    {
                        sonuc = new { hata = true, mesaj = "Firma bilgilerini giriniz", res =  "/Home/Index" };

                    }else if (usr.Id==-2)
                    {
                        sonuc = new { hata = true, mesaj = "Emailinizi onaylayınız", res =  "/Home/Index" };

                    }
                   else if (usr.Id > 0)
                    {
                        if (usr.Role== EnumRol.Tuketici || usr.Role == EnumRol.Tedarikci)
                        {
                            if (user.Password.Equals(XamarinUtils.SifreCoz(usr.Password)))
                            {
                                user.Password = "";
                                HttpContext.Session.SetObjectAsJson("AktifKullanici", usr);
                                TempData["adsoyad"] = usr.Name + " " + usr.Surname;
                                TempData["username"] = usr.UserName;
                                TempData["rol"] = usr.Role;


                                sonuc = new { hata = false, mesaj = "Giriş İşleminiz Başarılı. Lütfen Bekleyiniz", res =  "/Home/Index" };
                            }
                            else
                            {
                                sonuc = new { hata = true, mesaj = "Yanlış Şifre", res =  "/" };
                                TempData["girisdurum"] = "Şifrenizi Yanlış Girdiniz";

                                HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                            }

                        }
                        else
                        {

                            sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res =  "/" };
                        }
                    }
                    else
                    {
                        sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res =  "/" };
                        TempData["girisdurum"] = "Böyle bir kullanıcı Bulunamadı";

                        HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                    }


                }
                else
                {
                    sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res =  "/" };
                    TempData["girisdurum"] = "Böyle bir kullanıcı Bulunamadı";

                    HttpContext.Session.SetObjectAsJson("AktifKullanici", null);
                }

            }
            catch (Exception e)
            {

            }

            return Json(sonuc);

        }
        [HttpPost]
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        private IActionResult RedirectToLocal(string returnUrl) => RedirectToAction(nameof(HomeController.Index), "Oturum");


        public IActionResult TukMailOnaylama(string code)
        {
            PublicModel pm = new PublicModel();
            var usr = _websiteService.kayitKoduKontrol(code,EnumRol.Tedarikci);
            
                pm.user = null;
                return View(pm);
            

        }
        public IActionResult TedMailOnaylama(string code)
        {
            var usrses = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            PublicModel pm = new PublicModel();

            var usr = _websiteService.kayitKoduKontrol(code,EnumRol.Tedarikci);

            if (usr != null)
            {
                pm.ulkeler = _websiteService.getCountryList();
                pm.sirkettipleri = _websiteService.getCompanyTypeList();
                pm.userbilg = usr;
                pm.user = usrses;
                return View(pm);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult HesapOnayBilgi()
        {
            PublicModel pm = new PublicModel();
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
            
            if (usr==null)
            {
                pm.user = usr;
                return View(pm);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

        }//var kod= HttpContext.Session.GetString("emailkodu");
        public IActionResult MailHata()
        {
            PublicModel pm = new PublicModel();
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");

            if (usr == null)
            {
                pm.user = usr;
                return View(pm);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public async Task<JsonResult> tekrarMailYolla()
        {

            var sonuc = new { hata = true, mesaj = "Teknik Ariza", res = "/" };
            try
            {
                bool mesaj = false;
                var kod = HttpContext.Session.GetString("emailkodu");
                var email = HttpContext.Session.GetString("email");
                var rol = HttpContext.Session.GetString("rol");
                var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
                if (rol == EnumRol.Tedarikci.ToString())
                {
                     mesaj = send(email, "Üyeliğinizi başlatmak için tıklayınız: https://www.dijitalgidaagi.com/Login/TedMailOnaylama?code=" + kod + " ", "Mail Onaylama");
                }
                else
                {
                     mesaj = send(email, "Üyeliğinizi başlatmak için tıklayınız: https://www.dijitalgidaagi.com/Login/TukMailOnaylama?code=" + kod + " ", "Mail Onaylama");

                }
                if (mesaj)
                {
                    sonuc = new { hata = false, mesaj = "Mail gönderildi", res = "" + "/" };                
                }
                else
                {
                    sonuc = new { hata = true, mesaj = " Hata oluştu", res = "" + "/" };

                }


            }
            catch (Exception e)
            {

            }

            return Json(sonuc);

        }


        public static bool send(string pTo, string pBody, string pSubject)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            MailMessage mm = new MailMessage();
            mm.To.Add(pTo);
            mm.Body = pBody;
            mm.Subject = pSubject;
            mm.IsBodyHtml = true;
            mm.Sender = new MailAddress(
                 config["AppSettings:SendMailMessagesFromAddress"].ToString()
                );
            mm.From = new MailAddress(

                config["AppSettings:SendMailMessagesFromAddress"].ToString()
               );
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Host = config["AppSettings:SendMailSMTPHostAddress"].ToString();
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = Convert.ToBoolean(config["AppSettings:SendMailUseDefaultCredentials"]);
                sc.EnableSsl = Convert.ToBoolean(config["AppSettings:SendMailSMTPSSL"]);
                sc.Port = 587;
                sc.Port = Convert.ToInt32(config["AppSettings:SendMailSMTPPort"].ToString());

                sc.Credentials = new System.Net.NetworkCredential(

                config["AppSettings:SendMailSMTPUserName"].ToString(),
                config["AppSettings:SendMailSMTPUserPassword"].ToString()
                );

                sc.Send(mm);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        [HttpPost]
        public async Task<JsonResult> UploderDokuman(IList<IFormFile> file)
        {
            try
            {

                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var webRootPath = config["AppSettings:publicPath"].ToString();

                var filelist = file;
                var yeniisim = Guid.NewGuid().ToString();

                if (filelist.Count > 0)
                {
                    foreach (var item in filelist)
                    {
                        //string[] sn = item.FileName.Split('.');
                        yeniisim = yeniisim + item.FileName;
                        var fileName = "pdf\\" + yeniisim;
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
        public async Task<JsonResult> UploderImage(IList<IFormFile> files)
        {
            try
            {

                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var webRootPath = config["AppSettings:publicPath"].ToString();

                var filelist = files;
                var yeniisim = Guid.NewGuid().ToString();

                if (filelist.Count > 0)
                {
                    foreach (var item in filelist)
                    {
                        string[] sn = item.FileName.Split('.');
                        yeniisim = yeniisim + "." + sn[sn.Length - 1];
                        var fileName = "logo\\" + yeniisim;
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
        public async Task<JsonResult> firmaBilgiKaydet(CompanyDTO c)
        {

            var sonuc = new { hata = true, mesaj = "Teknik Ariza", res = "/" };
            try
            {
                if (c.Id<=0)
                {
                c.CreateDate = DateTime.Now;
                c.CreatedBy = c.UserId;

                }
                c.LastUpdateDate = DateTime.Now;
                c.LastUpdatedBy = c.UserId;
				c.LastIP = HttpContext.Connection.RemoteIpAddress?.ToString();
				var com = _websiteService.createFirma(c);
                if (com != null)
                {
                    if (c.Id > 0)
                    {

                        sonuc = new { hata = false, mesaj = "Firma bilgileri güncellendi. ", res = "" + "/" };
                    }
                    else
                    {
                        sonuc = new { hata = false, mesaj = "Firma bilgileri kaydedildi. Hesabınız aktif edildi", res =  "/" };
                    }

                }
                else
                {
                    sonuc = new { hata = true, mesaj = " Kayıt oluştururken hata oluştu", res = "" + "/" };

                }


            }
            catch (Exception e)
            {

            }

            return Json(sonuc);

        }
        public JsonResult getCity(int CountryId)
        {
            var getCityList = _websiteService.listCities(CountryId);
            SelectList listCity = new SelectList(getCityList, "Id", "Name");
            return Json(listCity);
        }
        public JsonResult getDistrict(int CityId)
        {
            var listDistrict = _websiteService.listDistrict(CityId);
            SelectList listDist = new SelectList(listDistrict, "Id", "Name");
            return Json(listDist);
        }
        [HttpPost]
        public async Task<JsonResult> ForgotPass(UserDTO user)
        {

            var sonuc = new { hata = true, mesaj = "Teknik Ariza", res = "/" };
            try
            {
                if (user.Email != null)
                {
                    if (user.Id==-1)
                    {
                        user.Id = 0;
                        user.Email = HttpContext.Session.GetString("email");
                    }
                    Random generator = new Random();
                    String r = generator.Next(0, 1000000).ToString("D6");

                    user.Password = XamarinUtils.Sifrele(r);
                    var usr = _websiteService.sifreYenile(user.Email, user.Password);

                    if (usr != null)
                    {

                        if (usr.Id>0)
                        {
                            bool mesaj = send(usr.Email, "Merhaba " + usr.Name + " yeni şifreniz <b>" + r + "</b>", "Şifre Yenileme");
                            if (mesaj)
                            {
                                sonuc = new { hata = false, mesaj = "Şifreniz güncellendi. Yeni şifrenizi gönderdiğimiz mailden öğrenebilirsiniz.", res = "/" };

                            }
                            else
                            {
                                sonuc = new { hata = false, mesaj = "Böyle bir kullanıcı Bulunamadı", res = "/" };
                            }

                        }
                        else
                        {
                            sonuc = new { hata = true, mesaj = "Böyle bir kullanıcı Bulunamadı", res = "/" };
                        }

                    }
                    else
                    {
                        sonuc = new { hata = true, mesaj = "Hata oluştu", res = "/" };
                    }


                }
                else
                {
                    sonuc = new { hata = true, mesaj = "Mail Adresi girilmedi", res = "/" };
                }

            }
            catch (Exception e)
            {

            }

            return Json(sonuc);

        }
    }
}
