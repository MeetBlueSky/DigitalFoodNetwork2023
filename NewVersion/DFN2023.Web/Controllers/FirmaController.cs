using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Web.Helpers;
using DFN2023.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFN2023.Web.Controllers
{
    public class FirmaController : Controller
    {
        private readonly IWebsiteService _websiteService;
        public FirmaController(IWebsiteService parametreService)
        {
            _websiteService = parametreService;
        }
        public IActionResult Detay(int id)
        {
            PublicModel pm = new PublicModel();
            var usr = HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");

            
                var a = _websiteService.getCompanyDetay(id, usr == null ? 0 : usr.Id);

                if (a !=null)
            {
                pm.sirket = a;
                pm.urunler = _websiteService.getUrunlerList(a.Id);
				List<DropDownDT> currency = new();
				List<DropDownDT> unit = new();
				currency.Add(new()
				{
					Value = CurrencyType.TL,
					Name = "TL"
				});
				currency.Add(new()
				{
					Value = CurrencyType.USD,
					Name = "USD"
				});
				currency.Add(new()
				{
					Value = CurrencyType.EUR,
					Name = "EUR"
				});
                
				for (int i = 0; i < pm.urunler.Count; i++)
                {
                    if (pm.urunler[i].Currency!=0)
					{
						pm.urunler[i].CurrencyName = currency.FirstOrDefault(x => x.Value == pm.urunler[i].Currency).Name;

                    }
                    else
                    {
                        pm.urunler[i].CurrencyName = "";
                    }

				}



				unit.Add(new()
				{
					Value = UnitType.Gr,
					Name = "Gr"
				});
				unit.Add(new()
				{
					Value = UnitType.Kg,
					Name = "Kg"
				});
				unit.Add(new()
				{
					Value = UnitType.Adet,
					Name = "Adet"
				});


				for (int i = 0; i < pm.urunler.Count; i++)
                {
                    if (pm.urunler[i].UnitId!=0)
					{
						pm.urunler[i].Unit = unit.FirstOrDefault(x => x.Value == pm.urunler[i].UnitId).Name;

                    }
                    else
                    {
                        pm.urunler[i].CurrencyName = "";
                    }

				}
                
                pm.user = usr;
                    return View(pm);
                }
                else
                {
                    return RedirectToAction("Home", "Index");
                }


        }
    }
}
