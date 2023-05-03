using DFN2023.Entities.DTO;

namespace DFN2023.Admin.Models
{
    public class ProductCompanyPageModel
    {

        public int lang { get; set; }
        public string language { get; set; }


        public List<CompanyDTO> Company { get; set; }
        public List<ProductBaseDTO> ProductBase { get; set; }
        public List<CategoryDTO> Category { get; set; }
    }
}
