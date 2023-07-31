
using DFN2023.Entities.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace DFN2023.Web.Controllers
{
    public class BaseController : Controller
    {

        [HttpGet]
        public ActionResult PartialViewYukle(string p_ViewAdi, string p_Controller)
        {
            return RedirectToAction(p_ViewAdi, p_Controller);
        }

        public int getUserId()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            return userId;
        }

        public void createSession(User usr)
        {
            HttpContext.Session.SetString("email", usr.Email);
            HttpContext.Session.SetString("adsoyad", usr.Name + " " + usr.Surname);
            HttpContext.Session.SetInt32("userId", usr.Id);
        }

        public void clearSession()
        {
            HttpContext.Session.Clear();
        }


        public void setLang(string langname)
        {
            if (langname == "tr")
            {
                CultureInfo.CurrentCulture = new CultureInfo("tr");
                CultureInfo.CurrentUICulture = new CultureInfo("tr");

            }
            else
            {
                CultureInfo.CurrentCulture = new CultureInfo("en");
                CultureInfo.CurrentUICulture = new CultureInfo("en");

            }
        }

        public int getLang(string langname)
        {

            if (langname.ToUpper() == "TR") return 1;
            else if (langname.ToUpper() == "EN") return 2;
            else return 1;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
