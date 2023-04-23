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
    public class SehirlerController : BaseController
    {
        private readonly IAdminService _adminService;
        public SehirlerController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "Şehirler";

            var getCountryList = _adminService.listCountries();
            SelectList listCountry = new SelectList(getCountryList, "Id", "Name");
            ViewBag.country = listCountry;

             

            return View();
        }

        [HttpPost]
        public Task<JsonResult> getSehir([FromBody] DtParameters dtParameters)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getCity(dtParameters, lang);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]
        public Task<JsonResult> addSehir(City city)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            city.LangId = lang;
            CityValidator vn = new CityValidator();
            ValidationResult result = vn.Validate(city);

            if (result.Errors.Count == 0)
            {
                var sonuc = _adminService.addCity(city);
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
        public Task<JsonResult> updateSehir(City city)
        {
            int lang = getLang(CultureInfo.CurrentCulture.Name);
            city.LangId = lang;
            CityValidator vn = new CityValidator();
            ValidationResult result = vn.Validate(city);

            if (result.Errors.Count == 0)
            {
                var sonuc = _adminService.updateCity(city);
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
        public Task<JsonResult> deleteSehir(City city)
        {
            try
            {
                _adminService.deleteCity(city);
                return Task.FromResult(Json("true"));
            }
            catch (Exception)
            {

                return Task.FromResult(Json("false"));
            }
        }

    }
}
