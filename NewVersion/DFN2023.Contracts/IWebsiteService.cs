﻿using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;

using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Contracts
{
    public interface IWebsiteService
    {
        UserDTO CheckUser(string email, string pass);
        List<CategoryDTO> getCategoryList();
        List<CompanyDTO> getTedarik(int kid, string? ürün,int? userid);
        //getCompanyList
        List<CompanyDTO> getCompanyList();
        List<StaticContentGrupPageDTO> getAnasayfaList();
        bool favMethod(int companyid, int userid, int pid);
        public List<CompanyDTO> getCompanyMap();
       // public MesajListDT getMesajList(int userid, bool hepsi);
        MesajListDT getMesajList(int userid, int start, int length,int role,int entedrol);
        List<MessageDTO> getMesajDetay(int userid, int fromid, int rolid, int start, int finish);
        bool mesajYazUser(Message m);
        string sirketOzelligi(int id, int role);
        User createUser(User user);
        User kayitKoduKontrol(string code,int rol);
        List<CountryDTO> getCountryList();
        List<City> listCities(int id);
        List<County> listDistrict(int id);
        Company createFirma(CompanyDTO cm);
        public List<ProductCompanyDTO> getUrunlerList(int sirketid);
        public ProductCompany createUrun(ProductCompanyDTO comp);
        public bool deleteUrun(int id);
        int getCompanyId(int userid);
        List<CompanyTypeDTO> getCompanyTypeList();
        User sifreYenile(string email, string code);
        CompanyDTO getCompanyDetay(int id, int userid);
        CompanyDTO getCompanyInfo(int userid);
        List<ProductBaseDTO> getUstUrunList();

        List<MenuManagementDTO> getMenuLayer1(int lang);

        /* Deep Blue Packages */
        // List<SlaytDTO> getSlaytForHome(int langId);

        StaticContentGrupPageDTO getStaticGrup(int pid, int lang);
        List<StaticContentPageDTO> getStaticContentPageList(int langId, int start, int grupID, int adet);
        List<StaticContentPageDTO> getStaticContentPageListMultiple(int langId, int start, int grupID, int grupID2, int adet);
        StaticContentPageDTO getStaticPage(int pid, int lang);

        List<StaticContentGrupPageDTO> getStaticContentPublications();
        List<StaticContentPageDTO> getStaticPageListByTempId(int tempID, int langId);

        List<StaticContentPageDTO> getStaticContentByGrupId(int groupId, int lang);
        StaticContentPageDTO getStaticContentByGrupIdLatest(int groupId, int lang);
        List<StaticContentPageDTO> getStaticContentByTempId(int tempId, int lang);


        // List<SlaytDTO> getSlayt();

        public List<StaticContentPageDTO> getNewsDetail(int newsId, int lang);


        // public List<MenuManagementDTO> getMenuLayer1(int lang);


        // public CommunicationForm createContantMessage(CommunicationFormDTO com);
        // public NewsLetter createNewsletter(NewsLetterDTO nl);


    }
}
