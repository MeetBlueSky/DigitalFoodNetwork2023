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
