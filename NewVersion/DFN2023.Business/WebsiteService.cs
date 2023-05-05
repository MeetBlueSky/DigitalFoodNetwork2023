using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using DFN2023.Common.Extentions;
using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Infrastructure.Repositories;
using DFN2023.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;

namespace DFN2023.Business
{
    public class WebsiteService : IWebsiteService
    {

        private readonly IUnitOfWork _unitOfWork;

        IRepository<User> _fkRepositoryUser;
        IRepository<Category> _fkRepositoryCategory;
        IRepository<ProductCompany> _fkRepositoryProductCompany;
        IRepository<Company> _fkRepositoryCompany;//StaticContentGrupPageDTO
        IRepository<StaticContentGrupPage> _fkRepositoryStaticContentGrupPage;
        IRepository<City> _fkRepositoryCity;
        IRepository<County> _fkRepositoryCounty;
        IRepository<UserUrunler> _fkRepositoryUserUrunler;

        IMapper _mapper;

        public WebsiteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;


            _fkRepositoryUser = _unitOfWork.GetRepostory<User>();
            _fkRepositoryCategory = _unitOfWork.GetRepostory<Category>();
            _fkRepositoryProductCompany = _unitOfWork.GetRepostory<ProductCompany>();
            _fkRepositoryCompany = _unitOfWork.GetRepostory<Company>();
            _fkRepositoryStaticContentGrupPage = _unitOfWork.GetRepostory<StaticContentGrupPage>();
            _fkRepositoryCity = _unitOfWork.GetRepostory<City>();
            _fkRepositoryCounty = _unitOfWork.GetRepostory<County>();
            _fkRepositoryUserUrunler = _unitOfWork.GetRepostory<UserUrunler>();

            //_fkRepositoryStaticContentPage = _unitOfWork.GetRepostory<StaticContentPage>();
            //_fkRepositoryStaticContentGrupPage = _unitOfWork.GetRepostory<StaticContentGrupPage>();
            //_fkRepositoryUserAccount = _unitOfWork.GetRepostory<UserAccount>();

        }


        public List<UserDTO> CheckUser(string uname, string pass)
        {
            var a = _mapper.Map<List<UserDTO>>(_fkRepositoryUser.Entities.Where(p => p.UserName == uname && p.Status == 1).ToList());
            return a;

        }



        public List<CategoryDTO> getCategoryList()
        {
            return _mapper.Map<List<CategoryDTO>>(_fkRepositoryCategory.Entities.Where(p => p.Status == 1).OrderBy(p => p.RowNum).ToList());
        }
        public List<ProductCompanyDTO> getTedarik(int kid, string ürün,int? userid)
        {
            try
            {
                var data = _fkRepositoryProductCompany.Entities
                .Where(x=>x.CategoryId==kid && x.Name.ToUpper().Contains(ürün.ToUpper()))
                .Select(p => new ProductCompanyDTO
                {
                    Id = p.Id,
                    Name=p.Name,
                    CategoryId=p.CategoryId,
                    CategoryName=p.Category.Name,
                    Code=p.Code,
                    CompanyId=p.CompanyId,
                    CompanyName=p.Company.OfficialName,
                    CreateDate=p.CreateDate,
                    LastUpdateDate=p.LastUpdateDate,
                    CreatedBy=p.CreatedBy,
                    LastUpdatedBy=p.LastUpdatedBy,
                    LastIP=p.LastIP,
                     Currency=p.Currency,
                     Price=p.Price,
                    RowNum=p.RowNum,
                    Status=p.Status,
                    ShortDesc=p.Company.ShortDescription,
                    CityName = _fkRepositoryCity.Entities.First(c => c.Id == p.Company.OfficialCityId).Name,
                    CountyName = _fkRepositoryCounty.Entities.First(c => c.Id == p.Company.OfficialCountyId).Name,
                    FavDurum = _fkRepositoryUserUrunler.Entities.Count()>0?_fkRepositoryUserUrunler.Entities.Where(c => c.CompanyId == p.CompanyId && c.UserId==userid).Count()>0?true:false:false,
                    //Null kontrollerini unutma

                }).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<CompanyDTO> getCompanyList()
        {
            List < CompanyDTO> a = new();
            try
            {
                var ab = _mapper.Map<List<CompanyDTO>>(_fkRepositoryCompany.Entities.Where(p => p.Status == 1).OrderByDescending(p => p.CreateDate).Take(4).ToList());//.OrderByDescending(p => p.CreateDate).Take(4)
                return ab;
            }
            catch (Exception)
            {

                return a;
            }
            
        }

        public List<StaticContentGrupPageDTO> getAnasayfaList()
        {
            List<StaticContentGrupPageDTO> a = new();
            try
            {
                var ab = _mapper.Map<List<StaticContentGrupPageDTO>>(_fkRepositoryStaticContentGrupPage.Entities.Where(p => p.GrupTempId==1 || p.GrupTempId == 3 || p.GrupTempId == 4 || p.GrupTempId == 6).ToList());//.OrderByDescending(p => p.CreateDate).Take(4)
                return ab;
            }
            catch (Exception)
            {

                return a;
            }

        }
        public CompanyDTO getSelectCompanyMap(int pid)
        {
            try
            {
                var a = _mapper.Map<CompanyDTO>(_fkRepositoryCompany.Entities.First(p => p.Id == pid));
                return a;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public List<CompanyDTO> getCompanyMap()
        {
            try
            {
                var a = _mapper.Map<List<CompanyDTO>>(_fkRepositoryCompany.Entities.Where(p => p.Status == 1).ToList());
                return a;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public bool favMethod(int companyid,int userid,int durum)
        {
            try
            {

                if (durum > 0)
                {
                    UserUrunler u = _fkRepositoryUserUrunler.Entities.First(x=>x.CompanyId== companyid);
                    _fkRepositoryUserUrunler.Delete(u);
                    _unitOfWork.SaveChanges();
                    return true;
                }
                else
                {
                    _fkRepositoryUserUrunler.Add(new()
                    {
                        UserId = userid,
                        CompanyId = companyid,
                        CreatedDate = DateTime.Now,
                    });
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}