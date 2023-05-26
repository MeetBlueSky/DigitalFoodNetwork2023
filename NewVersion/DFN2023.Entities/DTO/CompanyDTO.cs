using DFN2023.Entities.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFN2023.Entities.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string OfficialName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }
        public int CompanyTypeId { get; set; }
        public string OfficialAddress { get; set; }
        public int? OfficialCountryId { get; set; }
        public int? OfficialCityId { get; set; }
        public int? OfficialCountyId { get; set; }
        public string MapAddress { get; set; }
        public int? MapCountryId { get; set; }
        public int? MapCityId { get; set; }
        public int? MapCountyId { get; set; }
        public string? MapX { get; set; }
        public string? MapY { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int? YearFounded { get; set; }
        public string? Logo { get; set; }
        public string? Attachment { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Tiktok { get; set; }
        public string? Youtube { get; set; }
        public string? Whatsapp { get; set; }
        public string? Website { get; set; }
        public string? AdminNotes { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastIP { get; set; }
        public int Status { get; set; }



        public string? OfficialCountryName { get; set; }
        public string? OfficialCityName { get; set; }
        public string? OfficialCountyName { get; set; }
        public string? MapCountryName { get; set; }
        public string? MapCityName { get; set; }
        public string? MapCountyName { get; set; }


        public string? CompanyTypeName { get; set; }
        public int? UserId { get; set; }
        public bool? Fav { get; set; }
		public List<CompanyImage>? CompanyImage { get; set; }

	}
}
