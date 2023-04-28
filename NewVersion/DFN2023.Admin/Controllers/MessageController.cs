//using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using DFN2023.Contracts;
using DFN2023.Entities.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DFN2023.Entities.EF;


using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
//using OfficeOpenXml.Style;
//using OfficeOpenXml;
using System.Drawing;
using System.Globalization;
using DFN2023.Admin.Controllers;
using DFN2023.Admin.Models;
using DFN2023.Contracts;
using DFN2023.Entities.Models;
using System.ComponentModel;

namespace DFN2023.Admin.Controllers
{
    public class MessageController : BaseController
    {
        private readonly IAdminService _adminService;
        public MessageController(IAdminService parametreService)
        {
            _adminService = parametreService;
        }

        public IActionResult Index()
        {
            ViewData["page"] = "Kontak Formları";
            return View();
        }

        [HttpPost]
        public Task<JsonResult> getKontakFormlar([FromBody] DtParameters dtParameters)
        {
            // int lang = getLang(CultureInfo.CurrentCulture.Name);
            var sonuc = _adminService.getContactForms(dtParameters, 1);
            return Task.FromResult(Json(sonuc));
        }

        [HttpPost]
        public Task<JsonResult> SilIletisim(Message cf)
        {
            try
            {
                _adminService.deleteIletisim(cf);
                return Task.FromResult(Json("true"));
            }
            catch (Exception)
            {

                return Task.FromResult(Json("false"));
            }
        }
        [HttpPost]
        public Task<JsonResult> guncelleIletisim(Message cf)
        {
            try
            {
                _adminService.guncelleIletisim(cf);
                return Task.FromResult(Json("true"));
            }
            catch (Exception)
            {

                return Task.FromResult(Json("false"));
            }
        }

        //public IActionResult writeToExcel()
        //{
        //    // query data from database   
        //    List<ContactForm> list = _adminService.listContactForm();
        //    //Helpers.ExcelHandler.ListToExcel(cfList);

        //    var stream = new MemoryStream();
        //    //required using OfficeOpenXml;
        //    // If you use EPPlus in a noncommercial context
        //    // according to the Polyform Noncommercial license:
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    using (var package = new ExcelPackage(stream))
        //    {

        //        var workSheet = package.Workbook.Worksheets.Add("Kontak Formları");

        //        // setting the properties
        //        // of the work sheet 
        //        workSheet.TabColor = System.Drawing.Color.Black;
        //        workSheet.DefaultRowHeight = 12;

        //        // Setting the properties
        //        // of the first row
        //        workSheet.Row(1).Height = 20;
        //        workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        workSheet.Row(1).Style.Font.Bold = true;

        //        // Headers of the Excel sheet
        //        // By default, the column width is not 
        //        // set to auto fit for the content
        //        // of the range, so we are using
        //        int headerCount = 0;
        //        string[] str = new string[12];
        //        foreach (PropertyInfo item in typeof(Entities.EF.ContactForm).GetProperties())
        //        {
        //            str[headerCount] = item.Name;
        //            ++headerCount;
        //            workSheet.Cells[1, headerCount].Value = item.Name;
        //            workSheet.Cells[1, headerCount].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            workSheet.Cells[1, headerCount].Style.Font.Color.SetColor(System.Drawing.Color.White);
        //            workSheet.Cells[1, headerCount].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Navy);
        //        }

        //        // Inserting the article data into excel
        //        // sheet by using the for each loop
        //        // As we have values to the first row 
        //        // we will start with second row
        //        int recordIndex = 2;

        //        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        //        foreach (var classInList in list)
        //        {
        //            //PropertyInfo[] properties = typeof(Entities.EF.ContactForm).GetProperties();
        //            //for (int i = 0; i < headerCount; i++)
        //            //{
        //            if (recordIndex % 2 == 1)
        //            {
        //                using (ExcelRange Rng = workSheet.Cells[recordIndex, 1, recordIndex, headerCount])
        //                {

        //                    Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    Rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
        //                }
        //            }

        //            workSheet.Cells[recordIndex, 1].Value = classInList.Id;
        //            workSheet.Cells[recordIndex, 2].Value = textInfo.ToTitleCase(classInList.Name);
        //            workSheet.Cells[recordIndex, 3].Value = classInList.Email;
        //            workSheet.Cells[recordIndex, 4].Value = classInList.Phone;
        //            workSheet.Cells[recordIndex, 5].Value = classInList.Message;
        //            workSheet.Cells[recordIndex, 6].Value = classInList.KVKK1;
        //            workSheet.Cells[recordIndex, 7].Value = classInList.KVKK2;
        //            workSheet.Cells[recordIndex, 8].Value = classInList.Date.ToShortDateString() + " " + classInList.Date.ToLongTimeString();
        //            workSheet.Cells[recordIndex, 9].Value = (classInList.LangId == 1) ? "Türkçe" : "İngilizce";
        //            workSheet.Cells[recordIndex, 10].Value = StatusType(classInList.Status);
        //            workSheet.Cells[recordIndex, 11].Value = FormType(classInList.FormId);
        //            workSheet.Cells[recordIndex, 12].Value = classInList.IPAddress;

        //            recordIndex++;
        //        }

        //        workSheet.Cells[1, 1].Value = "NUMARA";
        //        workSheet.Cells[1, 2].Value = "ADI SOYADI";
        //        workSheet.Cells[1, 3].Value = "E-MAİL";
        //        workSheet.Cells[1, 4].Value = "TELEFON NO";
        //        workSheet.Cells[1, 5].Value = "NOT";
        //        workSheet.Cells[1, 6].Value = "KVK1";
        //        workSheet.Cells[1, 7].Value = "KVK2";
        //        workSheet.Cells[1, 8].Value = "GİRİŞ TARİHİ";
        //        workSheet.Cells[1, 9].Value = "DİL";
        //        workSheet.Cells[1, 10].Value = "STATÜ";
        //        workSheet.Cells[1, 11].Value = "FORM";
        //        workSheet.Cells[1, 12].Value = "IP ADRESİ";

        //        // AutoFit() method here. 
        //        for (int i = 1; i <= headerCount; i++)
        //        {
        //            workSheet.Column(i).AutoFit();
        //        }

        //        package.Save();
        //    }
        //    stream.Position = 0;
        //    string excelName = $"Kontak Formları-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
        //    return File(stream, "application/octet-stream", excelName);
        //    //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        //}


        //private static string FormType(int formId)
        //{
        //    switch (formId)
        //    {
        //        case 1:
        //            return "İletisim Sayfası";
        //        case 2:
        //            return "İzmir Mağaza";
        //        case 3:
        //            return "Adana Mağaza";
        //        case 4:
        //            return "Maslak Mağaza";
        //        case 5:
        //            return "Bostancı Mağaza";
        //        default:
        //            return "Form yok";
        //    }
        //}

        private static string StatusType(int status)
        {
            switch (status)
            {
                case 1:
                    return "Görülmedi";

                case 2:
                    return "Görüldü";

                default:
                    return "İşlem Yok";

            };
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
