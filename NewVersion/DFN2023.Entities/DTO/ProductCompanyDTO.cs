using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class ProductCompanyDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public int ProductBaseId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public float? Price { get; set; }
        public int? Currency { get; set; }
        public int RowNum { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastIP { get; set; }
        public int Status { get; set; }

        public string? ShortDesc { get; set; }
        public string? Desc { get; set; }
        

        public string? CompanyName { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductBaseName { get; set; }
        public string? CityName { get; set; }
        public string? CountyName { get; set; }
        public bool? FavDurum { get; set; }
        public int UserId { get; set; }
        public int? UnitId { get; set; }
        public string? Unit { get; set; }


        public string BrandName { get; set; }
        public string OfficialName { get; set; }
        public string ShortDescription { get; set; }
        public string MapAddress { get; set; }
        public string? MapX { get; set; }
        public string? MapY { get; set; }
        public string? Logo { get; set; }

    }
}
