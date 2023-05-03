using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;

namespace DFN2023.Web.Models
{
    public class PublicModel
    {
        public User? user { get; set; }
        public List<CategoryDTO>? kategoriler { get; set; }
        public List<ProductCompanyDTO>? tedariklist { get; set; }
        public List<CompanyDTO>? sirketler { get; set; }

    }
}