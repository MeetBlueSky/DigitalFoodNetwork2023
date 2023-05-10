using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;

using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Contracts
{
    public interface IWebsiteService
    {
        List<UserDTO> CheckUser(string uname, string pass);
        List<CategoryDTO> getCategoryList();
        List<ProductCompanyDTO> getTedarik(int kid, string? ürün,int? userid);
        //getCompanyList
        List<CompanyDTO> getCompanyList();
        List<StaticContentGrupPageDTO> getAnasayfaList();
        bool favMethod(int companyid, int userid, int pid);
        public List<CompanyDTO> getCompanyMap();
        public MesajListDT getMesajList(int userid, int role);
        List<MessageDTO> getMesajDetay(int userid, int fromid, int rolid);
        bool mesajYazUser(Message m);
        string sirketOzelligi(int id, int role);
    }
}
