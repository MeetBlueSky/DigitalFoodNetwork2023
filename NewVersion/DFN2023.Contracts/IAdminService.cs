using DFN2023.Entities.DTO; 
using DFN2023.Entities.EF;
using DFN2023.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text; 

namespace DFN2023.Contracts
{
    public interface IAdminService
    {
        List<UserDTO> CheckUser(string uname, string pass);


        public DtResult<UserDT> getUsers(DtParameters dtParameters);
        public User createUser(User usr);
        public bool deleteUser(User usr);

        //Category
        Category createKategori(Category cat);
        bool deleteKategori(Category cat);
        DtResult<CategoryDTO> getCategory(DtParameters dtParameters, int lang);
        public List<CategoryDTO> getcategoryList(int lang);

        // Company - CompanyType - CompanyImage
        Company createCompany(Company cat);
        bool deleteCompany(Company cat);
        List<CompanyTypeDTO> getCompanyTypeList(int lang);
        List<CountryDTO> getCountryList(int lang);
        List<CityDTO> getCityList(int lang);
        List<CountyDTO> getCountyList(int lang);
        DtResult<CompanyDTO> getCompany(DtParameters dtParameters, int lang);
        CompanyType createCompanyType(CompanyType cat);
        bool deleteCompanyType(CompanyType cat);
        public DtResult<CompanyImageDTO> getCompanyImage(DtParameters dtParameters);
        DtResult<CompanyTypeDTO> getCompanyType(DtParameters dtParameters, int lang);
        public CompanyImage createCompanyImage(CompanyImageDTO companyImage);
        public CompanyImage createCompanyImageNonDTO(CompanyImage companyImage);
        public bool deleteCompanyImage(CompanyImageDTO companyImage);


        //City - Country - County
        DtResult<CountryDT> getCountry(DtParameters dtParameters, int lang);
        DtResult<CityDT> getCity(DtParameters dtParameters, int lang);
        DtResult<CountyDT> getCounty(DtParameters dtParameters, int lang);
        List<City> listCities(int cityId);
        List<City> listCities();
        List<Country> listCountries();
        List<County> listCounties(int countyId);
        bool addCountry(Country country);
        bool updateCountry(Country country);
        bool deleteCountry(Country country);
        bool addCity(City city);
        bool updateCity(City city);
        bool deleteCity(City city);
        bool addCounty(County county);
        bool updateCounty(County county);
        bool deleteCounty(County county);

        //Message

        DtResult<MessageDTO> getContactForms(DtParameters dtParameters, int lang);
        bool guncelleIletisim(Message cf);
        bool deleteIletisim(Message cf);


        // ProductBase - ProductCompany
        ProductBase createProductBase(ProductBase cat);
        bool deleteProductBase(ProductBase cat);
        DtResult<ProductBaseDTO> getProductBase(DtParameters dtParameters, int lang);
        ProductCompany createProductCompany(ProductCompany cat);
        bool deleteProductCompany(ProductCompany cat);
        DtResult<ProductCompanyDTO> getProductCompany(DtParameters dtParameters, int lang);
        public List<CompanyDTO> getCompanyList(int lang);
        public List<ProductBaseDTO> getProductBaseList(int lang);
        public List<CategoryDTO> getCategoryList(int lang);



        //StaticContentGrupPage
        DtResult<StaticContentGrupPageDT> getStaticContentGrupPage(DtParameters dtParameters, int lang);
        DtResult<StaticContentGrupPageDT> getStaticContentGrupPageDashboard(DtParameters dtParameters, int lang);
        StaticContentGrupPage createStaticContentGrupPage(StaticContentGrupPage staticContent);
        bool deleteStaticContentGrupPage(StaticContentGrupPage staticContent);

        //StaticContentPage
        DtResult<StaticContentPageDT> getStaticContentPage(DtParameters dtParameters, int lang);
        DtResult<StaticContentPageDT> getStaticContentPageDashboard(DtParameters dtParameters, int lang);
        StaticContentPage createStaticContentPage(StaticContentPage staticContent);
        bool deleteStaticContentPage(StaticContentPage staticContent);

        //StaticContentGrupTemp
        DtResult<StaticContentGrupTempDT> getStaticContentGrupTemp(DtParameters dtParameters, int lang);
        StaticContentGrupTemp createStaticContentGrupTemp(StaticContentGrupTemp staticContent);
        bool deleteStaticContentGrupTemp(StaticContentGrupTemp staticContent);

        //StaticContentTemp
        DtResult<StaticContentTempDT> getStaticContentTemp(DtParameters dtParameters, int lang);
        StaticContentTemp createStaticContentTemp(StaticContentTemp staticContent);
        bool deleteStaticContentTemp(StaticContentTemp staticContent);

        //Burası
        List<StaticContentTemp> listStaticContentTemp();
        List<StaticContentGrupTemp> listStaticContentGrupTemp();
        List<StaticContentGrupPage> listStaticContentGrupPage(int lang);


        //Slider
        Slider createSlider(Slider pro);
        DtResult<SliderDT> getSlider(DtParameters dtParameters, int lang);
        bool deleteSlider(Slider slider);




    }
}
