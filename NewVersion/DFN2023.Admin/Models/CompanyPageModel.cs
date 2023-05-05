using DFN2023.Entities.DTO;

namespace DFN2023.Admin.Models
{
    public class CompanyPageModel
    {
        public int lang { get; set; }
        public string language { get; set; }

        public List<CompanyTypeDTO> CompanyType { get; set; }

        public List<CountryDTO> OfficialCountry { get; set; }
        public List<CityDTO> OfficialCity { get; set; }
        public List<CountyDTO> OfficialCounty { get; set; }
        public List<CountryDTO> MapCountry { get; set; }
        public List<CityDTO> MapCity { get; set; }
        public List<CountyDTO> MapCounty { get; set; }
    }
}
