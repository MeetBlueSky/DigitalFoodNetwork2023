using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;

using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Contracts
{
    public interface IWebsiteService
    {
        UserDTO CheckUser(string uname, string pass);
        List<CategoryDTO> getCategoryList();
        List<ProductCompanyDTO> getTedarik(int kid, string? ürün,int? userid);
        //getCompanyList
        List<CompanyDTO> getCompanyList();
        List<StaticContentGrupPageDTO> getAnasayfaList();
        bool favMethod(int companyid, int userid, int pid);
        public List<CompanyDTO> getCompanyMap();
       // public MesajListDT getMesajList(int userid, bool hepsi);
        MesajListDT getMesajList(int userid, int start, int length);
        List<MessageDTO> getMesajDetay(int userid, int fromid, int rolid, int start, int finish);
        bool mesajYazUser(Message m);
        string sirketOzelligi(int id, int role);
        User createUser(User user);
        User kayitKoduKontrol(string code);
        List<CountryDTO> getCountryList();
        List<City> listCities(int id);
        List<County> listDistrict(int id);
        Company createFirma(CompanyDTO cm);
        public List<ProductCompanyDTO> getUrunlerList(int userid);
        public ProductCompany createUrun(ProductCompanyDTO comp);
        public bool deleteUrun(int id);
        int getCompanyId(int userid);
    }
}
