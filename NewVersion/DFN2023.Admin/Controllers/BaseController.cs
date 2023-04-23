using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using DFN2023.Entities.DTO;
using DFN2023.Admin.Helpers;
using System.Globalization;

namespace DFN2023.Admin.Controllers
{
    public class BaseController : Controller
    {

        [HttpGet]
        public ActionResult PartialViewYukle(string p_ViewAdi, string p_Controller)
        {
            return RedirectToAction(p_ViewAdi, p_Controller);
        }


        public int getLang(string langname)
        {

            if (langname.ToUpper() == "TR") return 1;
            else if (langname.ToUpper() == "EN") return 2;
            else if (langname.ToUpper() == "FR") return 3;
            else return 1;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ak = HttpContext.Session.GetObjectFromJson<UserDTO>("AktifKullanici");


            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            if (actionName == "PartialViewYukle" || controllerName == "Oturum")
            {
                if (ak != null)
                {
                    if (actionName.Equals("Cikis")) { return; }
                    else
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            culture= CultureInfo.CurrentCulture.Name,
                            controller = "Home",
                            action = "Index"
                        }));
                    }
                }
                else
                {
                    return;
                }

            }


            if (ak == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    culture = CultureInfo.CurrentCulture.Name,
                    controller = "Oturum",
                    action = "Login"
                }));
            }
            else
            {

            }
        }
    }
}