using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;

namespace DFN2023.Web.Models
{
    public class PublicModel
    {
        public User? user { get; set; }
        public User? userbilg { get; set; }
        public List<CategoryDTO>? kategoriler { get; set; }
        public List<ProductBaseDTO>? usturun { get; set; }
        public List<CompanyDTO>? tedariklist { get; set; }
        public List<CompanyDTO>? sirketler { get; set; }
        public List<StaticContentGrupPageDTO>? stanasayfa { get; set; }
        public StaticContentGrupPageDTO? stanasayfatuk { get; set; }
        public StaticContentGrupPageDTO? stanasayfated { get; set; }
        public StaticContentGrupPageDTO? stanasayfasec1 { get; set; }
        public StaticContentGrupPageDTO? stanasayfasec2 { get; set; }
        public int? skategoriid { get; set; }
        public string? tedarikciadi { get; set; }
        public List<CompanyDTO>? mapkoor { get; set; }
        public MesajListDT? mesajlist { get; set; }
        public List<MessageDTO>? mesajdetay { get; set; }
        public int? selectcompanyid { get; set; }
        public int? mokunmamiscount { get; set; }
        public int? tedarikcirol { get; set; }
        public List<CountryDTO>? ulkeler { get; set; }
        public List<ProductCompanyDTO>? urunler { get; set; }
        public List<CompanyTypeDTO>? sirkettipleri { get; set; }
        public List<DropDownDT>? unitlist { get; set; }
        public List<DropDownDT>? currencylist { get; set; }
        public CompanyDTO sirket { get; set; }

        public StaticContentGrupPageDTO? StaticContentGrupPage { get; set; }
        public StaticContentPageDTO? StaticContentPage { get; set; }

        public List<StaticContentGrupPageDTO>? StaticContentGrupPageList { get; set; }
        public List<StaticContentPageDTO>? StaticContentPageList { get; set; }
        public List<StaticContentPageDTO>? StaticContentPageList2 { get; set; }

        public List<SliderDTO>? SlaytList { get; set; }


    }
}