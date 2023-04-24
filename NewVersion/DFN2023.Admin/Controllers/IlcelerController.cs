using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DFN2023.Business.Validators;
using DFN2023.Contracts;
using DFN2023.Entities.EF;
using DFN2023.Entities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DFN2023.Admin.Controllers
{
    public class IlcelerController : BaseController
    {
        private readonly IAdminService _adminService;
        public IlcelerController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "İlçeler";

            var getCountyList = _adminService.listCities();
            SelectList listCounty = new SelectList(getCountyList, "Id", "Name");
            ViewBag.city = listCounty;

             

            return View();
        }

        [HttpPost]
        public Task<JsonResult> getIlce([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getCounty(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]
        public Task<JsonResult> addIlce(County county)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            county.LangId = lang;
            IlceValidator vn = new IlceValidator();
            ValidationResult result = vn.Validate(county);

            if (result.Errors.Count == 0)
            {
                var sonuc = _adminService.addCounty(county);
                return Task.FromResult(Json("true"));
            }
            else
            {
                var s = "";
                foreach (var item in result.Errors)
                {
                    // ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    s += item.ErrorMessage.ToString() + "</br> ";

                }
                return Task.FromResult(Json(s));
            }

        }



        [HttpPost]
        public Task<JsonResult> updateIlce(County county)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            county.LangId = lang;
            IlceValidator vn = new IlceValidator();
            ValidationResult result = vn.Validate(county);

            if (result.Errors.Count == 0)
            {
                var sonuc = _adminService.updateCounty(county);
                return Task.FromResult(Json("true"));
            }
            else
            {
                var s = "";
                foreach (var item in result.Errors)
                {
                    // ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    s += item.ErrorMessage.ToString() + "</br> ";

                }
                return Task.FromResult(Json(s));
            }
        }


        [HttpPost]
        public Task<JsonResult> deleteIlce(County county)
        {
            try
            {
                _adminService.deleteCounty(county);
                return Task.FromResult(Json("true"));
            }
            catch (Exception)
            {

                return Task.FromResult(Json("false"));
            }
        }

    }
}
