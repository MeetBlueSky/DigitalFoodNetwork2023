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
    public class UlkelerController : BaseController
    {
        private readonly IAdminService _adminService;
        public UlkelerController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "Ülkeler"; 

            return View();
        }

        [HttpPost]
        public Task<JsonResult> getUlke([FromBody] DtParameters dtParameters)
        {
              int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getCountry(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]
        public Task<JsonResult> addUlke(Country country)
        {

            int lang = getLang(CultureInfo.CurrentCulture.Name);
            country.LangId = lang;
            CountryValidator vn = new CountryValidator();
            ValidationResult result = vn.Validate(country);

            if (result.Errors.Count == 0)
            {
                var sonuc = _adminService.addCountry(country);
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
        public Task<JsonResult> updateUlke(Country country)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            country.LangId = lang;
            CountryValidator vn = new CountryValidator();
            ValidationResult result = vn.Validate(country);

            if (result.Errors.Count == 0)
            {
                var sonuc = _adminService.updateCountry(country);
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
        public Task<JsonResult> deleteUlke(Country country)
        {
            try
            {
                _adminService.deleteCountry(country);
                return Task.FromResult(Json("true"));
            }
            catch (Exception)
            {

                return Task.FromResult(Json("false"));
            }
        }

    }
}
