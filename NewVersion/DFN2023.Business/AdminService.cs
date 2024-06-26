﻿using AutoMapper;

using Microsoft.EntityFrameworkCore;

using DFN2023.Common.Extentions;
using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Entities.Extention;
using DFN2023.Infrastructure.Repositories;
using DFN2023.Infrastructure.UnitOfWork;
using DFN2023.Entities.Models;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net.Http;

namespace DFN2023.Business
{
    public class AdminService : IAdminService
    {

        private readonly IUnitOfWork _unitOfWork;

        IRepository<Category> _fkRepositoryCategory;
        IRepository<CategoryProductBase> _fkRepositoryCategoryProductBase;
        IRepository<City> _fkRepositoryCity;
        IRepository<Company> _fkRepositoryCompany;
        IRepository<CompanyImage> _fkRepositoryCompanyImage;
        IRepository<CompanyType> _fkRepositoryCompanyType;
        IRepository<Country> _fkRepositoryCountry;
        IRepository<County> _fkRepositoryCounty;
        IRepository<Language> _fkRepositoryLanguage;
        IRepository<MenuManagement> _fkRepositoryMenuManagement;
        IRepository<Message> _fkRepositoryMessage;
        IRepository<ProductBase> _fkRepositoryProductBase;
        IRepository<ProductCompany> _fkRepositoryProductCompany;
        IRepository<Slider> _fkRepositorySlider;
        IRepository<SliderHeader> _fkRepositorySliderHeader;
        IRepository<StaticContentGrupPage> _fkRepositoryStaticContentGrupPage;
        IRepository<StaticContentGrupTemp> _fkRepositoryStaticContentGrupTemp;
        IRepository<StaticContentPage> _fkRepositoryStaticContentPage;
        IRepository<StaticContentTemp> _fkRepositoryStaticContentTemp;
        IRepository<User> _fkRepositoryUser;
        IRepository<UserUrunler> _fkRepositoryUserUrunler;


        IMapper _mapper;
        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _fkRepositoryCategory = _unitOfWork.GetRepostory<Category>();
            _fkRepositoryCategoryProductBase = _unitOfWork.GetRepostory<CategoryProductBase>();
            _fkRepositoryCity = _unitOfWork.GetRepostory<City>();
            _fkRepositoryCompany = _unitOfWork.GetRepostory<Company>();
            _fkRepositoryCompanyImage = _unitOfWork.GetRepostory<CompanyImage>();
            _fkRepositoryCompanyType = _unitOfWork.GetRepostory<CompanyType>();
            _fkRepositoryCountry = _unitOfWork.GetRepostory<Country>();
            _fkRepositoryCounty = _unitOfWork.GetRepostory<County>();
            _fkRepositoryLanguage = _unitOfWork.GetRepostory<Language>();
            _fkRepositoryMenuManagement = _unitOfWork.GetRepostory<MenuManagement>();
            _fkRepositoryMessage = _unitOfWork.GetRepostory<Message>();
            _fkRepositoryProductBase = _unitOfWork.GetRepostory<ProductBase>();
            _fkRepositoryProductCompany = _unitOfWork.GetRepostory<ProductCompany>();
            _fkRepositorySlider = _unitOfWork.GetRepostory<Slider>();
            _fkRepositorySliderHeader = _unitOfWork.GetRepostory<SliderHeader>();
            _fkRepositoryStaticContentGrupPage = _unitOfWork.GetRepostory<StaticContentGrupPage>();
            _fkRepositoryStaticContentGrupTemp = _unitOfWork.GetRepostory<StaticContentGrupTemp>();
            _fkRepositoryStaticContentPage = _unitOfWork.GetRepostory<StaticContentPage>();
            _fkRepositoryStaticContentTemp = _unitOfWork.GetRepostory<StaticContentTemp>();
            _fkRepositoryUser = _unitOfWork.GetRepostory<User>();
            _fkRepositoryUserUrunler = _unitOfWork.GetRepostory<UserUrunler>();


        }
        public List<UserDTO> CheckUser(string uname, string pass)
        {
            return _mapper.Map<List<UserDTO>>(_fkRepositoryUser.Entities.Where(p => p.UserName == uname && p.Status == 1).ToList());

        }


        public DtResult<UserDT> getUsers(DtParameters dtParameters)
        {
            try
            {

                Expression<Func<User, bool>> expProducts = c => true;
                //expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.UserName != null && r.UserName.ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.Surname != null && r.Surname.ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()));
                }
                var sql = _fkRepositoryUser.Include()
                .Where(expProducts)
                .Select(p => new UserDT
                {
                    Id = p.Id,
                    UserName = p.UserName,
                    Password = p.Password,
                    Name = p.Name,
                    Surname = p.Surname,
                    Email = p.Email,
                    Phone = p.Phone,
                    Role = p.Role,
                    Status = p.Status,
                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<UserDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }
        public User createUser(User usr)
        {
            try
            {

                if (usr.Id > 0)
                {
                    if (usr.Password == null || usr.Password.Length < 1)
                    {
                        var onceki = _fkRepositoryUser.Entities.AsNoTracking().FirstOrDefault(p => p.Id == usr.Id);
                        usr.Password = onceki.Password;
                    }

                    var result = _fkRepositoryUser.Update(usr);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    var result = _fkRepositoryUser.Add(usr);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public bool deleteUser(User usr)
        {
            try
            {
                _fkRepositoryUser.Delete(usr.Id);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        //Category 

        public DtResult<CategoryDTO> getCategory(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<Category, bool>> expProducts = c => true;
                expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryCategory.Include()
                .Where(expProducts)
                .Select(p => new CategoryDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Image = p.Image,
                    ParentId = p.ParentId,
                    ParentName = p.ParentCategory.Name,
                    RowNum = p.RowNum,
                    CreatedBy = p.CreatedBy,
                    LastUpdatedBy = p.LastUpdatedBy,
                    CreateDate = p.CreateDate,
                    LastUpdateDate = p.LastUpdateDate,
                    LastIP = p.LastIP,
                    Status = p.Status,
                    LangId = p.LangId,
                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<CategoryDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }
        public Category createKategori(Category cat)
        {
            try
            {

                if (cat.Id > 0)
                {
                    cat.LastUpdateDate = DateTime.Now;
                    cat.LastUpdatedBy = 1;
                    var result = _fkRepositoryCategory.Update(cat);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    cat.CreateDate = DateTime.Now;
                    cat.CreatedBy = 1;
                    var result = _fkRepositoryCategory.Add(cat);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool deleteKategori(Category cat)
        {
            try
            {
                _fkRepositoryCategory.Delete(cat.Id);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public List<CategoryDTO> getcategoryList(int lang)
        {
            return _mapper.Map<List<CategoryDTO>>(_fkRepositoryCategory.Entities.Where(p => p.Status == 1).ToList());
        }


        // Company - CompanyType - CompanyImage 

        public DtResult<CompanyDTO> getCompany(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<Company, bool>> expProducts = c => true;
                //expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.OfficialName != null && r.OfficialName.ToUpper().Contains(searchBy.ToUpper()));
                }

                //var sql = _fkRepositoryCompany.Include()
                //.Where(expProducts)
                //.Select(p => new CompanyDTO
                //{
                //    Id = p.Id,
                //    BrandName = p.BrandName,
                //    OfficialName = p.OfficialName,
                //    ShortDescription = p.ShortDescription,
                //    DetailDescription = p.DetailDescription,
                //    TaxOffice = p.TaxOffice,
                //    TaxNo = p.TaxNo,
                //    CompanyTypeId = p.CompanyTypeId,
                //    CompanyTypeName = p.CompanyType.TypeName, 
                //    OfficialAddress = p.OfficialAddress,
                //    OfficialCityId = p.OfficialCityId,
                //    OfficialCountryId = p.OfficialCountryId,
                //    OfficialCountyId = p.OfficialCountyId,
                //    MapAddress = p.MapAddress,
                //    MapCountryId = p.MapCountryId,
                //    MapCityId = p.MapCityId,
                //    MapCountyId = p.MapCountyId,
                //    MapX = p.MapX,
                //    MapY = p.MapY,
                //    Email = p.Email,
                //    Phone = p.Phone,
                //    Mobile = p.Mobile,
                //    YearFounded = p.YearFounded,
                //    Logo = p.Logo,
                //    Attachment = p.Attachment,
                //    Facebook = p.Facebook,
                //    Instagram = p.Instagram,
                //    Tiktok = p.Tiktok,
                //    Youtube = p.Youtube,
                //    Whatsapp = p.Whatsapp,
                //    Website = p.Website,
                //    AdminNotes = p.AdminNotes,
                //    CreatedBy = p.CreatedBy,
                //    LastUpdatedBy = p.LastUpdatedBy,
                //    CreateDate = p.CreateDate,
                //    LastUpdateDate = p.LastUpdateDate,
                //    LastIP = p.LastIP,
                //    Status = p.Status,
                //    /**/

                //}).AsQueryable();

                var sql2 = from company in _fkRepositoryCompany.Entities
                           .Where(expProducts)
                           from OfficialCountry in _fkRepositoryCountry.Entities
                               .Where(c => c.Id == company.OfficialCountryId).DefaultIfEmpty()
                           from OfficialCity in _fkRepositoryCity.Entities
                               .Where(c => c.Id == company.OfficialCityId).DefaultIfEmpty()
                           from OfficialCounty in _fkRepositoryCounty.Entities
                           .Where(c => c.Id == company.OfficialCountyId).DefaultIfEmpty()
                           from MapCountry in _fkRepositoryCountry.Entities
                               .Where(c => c.Id == company.MapCountryId).DefaultIfEmpty()
                           from MapCity in _fkRepositoryCity.Entities
                           .Where(c => c.Id == company.MapCityId).DefaultIfEmpty()
                           from MapCounty in _fkRepositoryCountry.Entities
                               .Where(c => c.Id == company.MapCountyId).DefaultIfEmpty()
                           select new CompanyDTO
                           {
                               Id = company.Id,
                               BrandName = company.BrandName,
                               OfficialName = company.OfficialName,
                               ShortDescription = company.ShortDescription,
                               DetailDescription = company.DetailDescription,
                               TaxOffice = company.TaxOffice,
                               TaxNo = company.TaxNo,
                               CompanyTypeId = company.CompanyTypeId,
                               CompanyTypeName = company.CompanyType.TypeName,

                               OfficialAddress = company.OfficialAddress,
                               OfficialCityId = company.OfficialCityId,
                               OfficialCountryId = company.OfficialCountryId,
                               OfficialCountyId = company.OfficialCountyId,
                               MapAddress = company.MapAddress,
                               MapCountryId = company.MapCountryId,
                               MapCityId = company.MapCityId,
                               MapCountyId = company.MapCountyId,

                               OfficialCountryName = OfficialCountry.Name,
                               OfficialCityName = OfficialCity.Name,
                               OfficialCountyName = OfficialCounty.Name,
                               MapCountryName = MapCountry.Name,
                               MapCityName = MapCity.Name,
                               MapCountyName = MapCounty.Name,

                               MapX = company.MapX,
                               MapY = company.MapY,
                               Email = company.Email,
                               Phone = company.Phone,
                               Mobile = company.Mobile,
                               YearFounded = company.YearFounded,
                               Logo = company.Logo,
                               Attachment = company.Attachment,
                               Facebook = company.Facebook,
                               Instagram = company.Instagram,
                               Tiktok = company.Tiktok,
                               Youtube = company.Youtube,
                               Whatsapp = company.Whatsapp,
                               Website = company.Website,
                               AdminNotes = company.AdminNotes,
                               CreatedBy = company.CreatedBy,
                               LastUpdatedBy = company.LastUpdatedBy,
                               CreateDate = company.CreateDate,
                               LastUpdateDate = company.LastUpdateDate,
                               LastIP = company.LastIP,
                               Status = company.Status,
                           };
                var sql = sql2.AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<CompanyDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }
        public Company createCompany(Company com)
        {
            try
            {

                if (com.Id > 0)
                {
                    com.LastUpdateDate = DateTime.Now;
                    com.LastUpdatedBy = 1;
                    var result = _fkRepositoryCompany.Update(com);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    com.LastUpdateDate = DateTime.Now;
                    com.LastUpdatedBy = 1;
                    var result = _fkRepositoryCompany.Add(com);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool deleteCompany(Company com)
        {
            try
            {
                _fkRepositoryCompany.Delete(com.Id);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public List<CompanyTypeDTO> getCompanyTypeList(int lang)
        {
            var list = _mapper.Map<List<CompanyTypeDTO>>(_fkRepositoryCompanyType.Entities.Where(p => p.Status == 1).ToList());
            return list;
        }

        public List<CountryDTO> getCountryList(int lang)
        {
            var list = _mapper.Map<List<CountryDTO>>(_fkRepositoryCountry.Entities.Where(p => p.Status == 1).ToList());
            return list;
        }
        public List<CityDTO> getCityList(int lang)
        {
            var list = _mapper.Map<List<CityDTO>>(_fkRepositoryCity.Entities.Where(p => p.Status == 1).ToList());
            return list;
        }

        public List<CountyDTO> getCountyList(int lang)
        {
            var list = _mapper.Map<List<CountyDTO>>(_fkRepositoryCounty.Entities.Where(p => p.Status == 1).ToList());
            return list;
        }

        public DtResult<CompanyTypeDTO> getCompanyType(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<CompanyType, bool>> expProducts = c => true;
                //expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.TypeName != null && r.TypeName.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryCompanyType.Include()
                .Where(expProducts)
                .Select(p => new CompanyTypeDTO
                {
                    Id = p.Id,
                    TypeName = p.TypeName,
                    Icon = p.Icon,
                    RowNum = p.RowNum,
                    CreatedBy = p.CreatedBy,
                    LastUpdatedBy = p.LastUpdatedBy,
                    CreateDate = p.CreateDate,
                    LastUpdateDate = p.LastUpdateDate,
                    Status = p.Status,
                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<CompanyTypeDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }
        public CompanyType createCompanyType(CompanyType cat)
        {
            try
            {
                //User usr =  HttpContext.Session.GetObjectFromJson<User>("AktifKullanici");
                //TempData

                if (cat.Id > 0)
                {
                    cat.LastUpdateDate = DateTime.Now;
                    cat.LastUpdatedBy = 1;
                    var result = _fkRepositoryCompanyType.Update(cat);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    cat.CreateDate = DateTime.Now;
                    cat.CreatedBy = 1;
                    var result = _fkRepositoryCompanyType.Add(cat);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool deleteCompanyType(CompanyType cat)
        {
            try
            {
                _fkRepositoryCompanyType.Delete(cat.Id);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public DtResult<CompanyImageDTO> getCompanyImage(DtParameters dtParameters)
        {
            try
            {
                Expression<Func<CompanyImage, bool>> expCompanyImage = c => true;
                expCompanyImage = expCompanyImage.And(p => p.CompanyId == dtParameters.FilterId);
                expCompanyImage = expCompanyImage.And(p => p.Status == 1 || p.Status == 2);
                //int boatSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(0));
                //if (boatSelectListValue != 0)
                //{
                //    expBoatImage = expBoatImage.And(p => p.ImageType == boatSelectListValue);
                //}

                var searchBy = dtParameters.Search?.Value;
                //if (!string.IsNullOrEmpty(searchBy))
                //{
                //    expBoatImage = expBoatImage.And(r => rç != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                //}

                var sql = _fkRepositoryCompanyImage.Entities
                .Where(expCompanyImage)
                .Select(p => new CompanyImageDTO
                {
                    Id = p.Id,
                    CompanyId = p.CompanyId,
                    Company = p.Company.OfficialName,
                    CreateDate = p.CreateDate,
                    //ImageType = p.ImageType,
                    Desc = p.Desc,
                    Path = p.Path,
                    LastUpdateDate = p.LastUpdateDate,
                    RowNum = p.RowNum,
                    Status = p.Status,
                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<CompanyImageDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public CompanyImage createCompanyImage(CompanyImageDTO companyImage)
        {
            try
            {
                var boat = _mapper.Map<CompanyImage>(companyImage);
                boat.LastUpdateDate = DateTime.Now;
                if (boat.Id > 0)
                {
                    var result = _fkRepositoryCompanyImage.Update(boat);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    boat.CreateDate = DateTime.Now;
                    var result = _fkRepositoryCompanyImage.Add(boat);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CompanyImage createCompanyImageNonDTO(CompanyImage companyImage)
        {
            try
            {
                //var boat = _mapper.Map<BoatImage>(boatImage);
                companyImage.LastUpdateDate = DateTime.Now;
                if (companyImage.Id > 0)
                {
                    var result = _fkRepositoryCompanyImage.Update(companyImage);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    companyImage.CreateDate = DateTime.Now;
                    var result = _fkRepositoryCompanyImage.Add(companyImage);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool deleteCompanyImage(CompanyImageDTO companyImage)
        {
            try
            {
                var boat = _fkRepositoryCompanyImage.GetById(companyImage.Id);
                boat.Status = 3;
                _fkRepositoryCompanyImage.Update(boat);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /*Country - City - County*/

        public List<City> listCities(int id)
        {
            var getCityList = _fkRepositoryCity.GetList(p => p.CountryId == id).ToList();
            return getCityList;
        }

        public DtResult<CityDT> getCity(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<City, bool>> expProducts = c => true;
                expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryCity.Include(p => p.Country)
                .Where(expProducts)
                .Select(p => new CityDT
                {
                    Id = p.Id,
                    Name = p.Name,
                    Status = p.Status,
                    LangId = p.LangId,
                    RowNum = p.RowNum,
                    CountryId = p.CountryId,
                    Country = p.Country.Name,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<CityDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool addCity(City city)
        {
            try
            {
                _fkRepositoryCity.Add(city);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool updateCity(City city)
        {
            try
            {
                _fkRepositoryCity.Update(city);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool deleteCity(City city)
        {
            try
            {
                _fkRepositoryCity.Delete(city);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public DtResult<CountryDT> getCountry(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<Country, bool>> expProducts = c => true;
                expProducts = expProducts.And(p => p.LangId == lang);
                //    expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryCountry.Include()
                .Where(expProducts)
                .Select(p => new CountryDT
                {
                    Id = p.Id,
                    Name = p.Name,
                    Status = p.Status,
                    LangId = p.LangId,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<CountryDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool addCountry(Country country)
        {
            try
            {
                _fkRepositoryCountry.Add(country);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool updateCountry(Country country)
        {
            try
            {
                _fkRepositoryCountry.Update(country);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool deleteCountry(Country country)
        {
            try
            {
                _fkRepositoryCountry.Delete(country);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public List<Country> listCountries()
        {
            var getCountryList = _fkRepositoryCountry.GetList().ToList();
            return getCountryList;
        }

        public List<City> listCities()
        {
            var getCityList = _fkRepositoryCity.GetList().ToList();
            return getCityList;
        }

        public List<County> listCounties(int id)
        {
            var getCountyList = _fkRepositoryCounty.GetList(p => p.CityId == id).ToList();
            return getCountyList;
        }

        public DtResult<CountyDT> getCounty(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<County, bool>> expProducts = c => true;
                expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryCounty.Include(p => p.City)
                .Where(expProducts)
                .Select(p => new CountyDT
                {
                    Id = p.Id,
                    Name = p.Name,
                    Status = p.Status,
                    LangId = p.LangId,
                    RowNum = p.RowNum,
                    CityId = p.CityId,
                    City = p.City.Name,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<CountyDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool addCounty(County county)
        {
            try
            {
                _fkRepositoryCounty.Add(county);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool updateCounty(County county)
        {
            try
            {
                _fkRepositoryCounty.Update(county);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public bool deleteCounty(County County)
        {
            try
            {
                _fkRepositoryCounty.Delete(County);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        //Language
        public string setDefaultLanguage()
        {
            return _fkRepositoryLanguage.Entities.Where(p => p.DefaultLang == true).FirstOrDefault().ShortName.ToLower();
        }

        public List<LanguageDTO> getLanguageList()
        {
            string currentLanguage = System.Globalization.CultureInfo.CurrentCulture.Name.ToUpper();

            List<LanguageDTO> languageList = new List<LanguageDTO>();
            languageList.Add(_mapper.Map<LanguageDTO>(_fkRepositoryLanguage.Entities.Where(p => p.Status == 1 && p.ShortName.ToUpper() == currentLanguage).FirstOrDefault()));
            languageList.AddRange(_mapper.Map<List<LanguageDTO>>(_fkRepositoryLanguage.Entities.Where(p => p.Status == 1 && p.ShortName.ToUpper() != currentLanguage).ToList()));

            return languageList;
        }


        //Menu
        public DtResult<MenuManagementDTO> getMenuManagement(DtParameters dtParameters, int lang)
        {
            try
            {
                Expression<Func<MenuManagement, bool>> expMenuManagement = c => true;
                expMenuManagement = expMenuManagement.And(p => p.LangId == lang);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expMenuManagement = expMenuManagement.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper())
                    || r.MenuLayerCode != null && r.MenuLayerCode.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryMenuManagement.Entities
                .Where(expMenuManagement)
                .Select(p => new MenuManagementDTO
                {
                    Id = p.Id,
                    Link = p.Link,
                    Image = p.Image,
                    ClickType = p.ClickType,
                    MenuFeatureId = p.ClickType,
                    MenuLayer = p.MenuLayer,
                    MenuLayerCode = p.MenuLayerCode,
                    Name = p.Name,
                    OpeningType = p.OpeningType,
                    ParentId = p.ParentId,
                    ChildId = p.ChildId,
                    RowNum = p.RowNum,
                    Status = p.Status,
                    LangId = p.LangId,

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<MenuManagementDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }
        public MenuManagement createMenuManagement(MenuManagement menu)
        {
            try
            {
                if (menu.Image == null)
                {
                    menu.Image = " ";
                }
                //var menuu = _mapper.Map<MenuManagement>(menu);
                if (menu.Id > 0)
                {
                    var result = _fkRepositoryMenuManagement.Update(menu);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {

                    var result = _fkRepositoryMenuManagement.Add(menu);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool deleteMenuManagement(int id)
        {
            try
            {
                var menuu = _fkRepositoryMenuManagement.GetById(id);
                _fkRepositoryMenuManagement.Delete(menuu);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }
        public List<MenuManagementDTO> getMenuParentList()
        {
            return _mapper.Map<List<MenuManagementDTO>>(_fkRepositoryMenuManagement.Entities.Where(p => p.Status == 1 && p.MenuLayer == 2).ToList());
        }
        public List<MenuManagementDTO> getMenuChildList()
        {
            return _mapper.Map<List<MenuManagementDTO>>(_fkRepositoryMenuManagement.Entities.Where(p => p.Status == 1 && p.MenuLayer == 3).ToList());
        }



        //Message

        public bool deleteIletisim(Message cf)
        {
            try
            {
                _fkRepositoryMessage.Delete(cf.Id);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool guncelleIletisim(Message cf)
        {
            try
            {
                var a = _fkRepositoryMessage.Entities.FirstOrDefault(p => p.Id == cf.Id);
                a.Status = cf.Status;
                _fkRepositoryMessage.Update(a);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public DtResult<MessageDTO> getContactForms(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<Message, bool>> expProducts = c => true;
                // expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    //expProducts = expProducts.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                    //                           r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()) ||
                    //                           r.Phone != null && r.Phone.ToUpper().Contains(searchBy.ToUpper()) ||
                    //                            r.Message != null && r.Message.ToUpper().Contains(searchBy.ToUpper()) ||
                    //                           r.Date != null && r.Date.ToString().ToUpper().Contains(searchBy.ToUpper()));
                    expProducts = expProducts.And(r => r.MessageContent != null && r.MessageContent.ToUpper().Contains(searchBy.ToUpper())
                            //r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                            //r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()) ||
                            //r.Phone != null && r.Phone.ToUpper().Contains(searchBy.ToUpper()) ||
                            // r.MessageContent != null && r.MessageContent.ToUpper().Contains(searchBy.ToUpper())
                            );
                }

                //var sql = _fkRepositoryMessage.Include()
                //.Where(expProducts)
                //.Select(p => new MessageDTO
                //{
                //    Id = p.Id,

                //    FromUser = p.FromUser,
                //    ToUser = p.ToUser,

                //    FromRolId = p.FromRolId,
                //    MessageContent = p.MessageContent,
                //    CreateDate = p.CreateDate,
                //    LastIP = p.LastIP,
                //    Status = p.Status,
                //    UserShow = p.UserShow,
                //    CompanyShow = p.CompanyShow,



                //}).AsQueryable();

                var sql2 = from messages in _fkRepositoryMessage.Entities
                           .Where(expProducts)
                           from fromUser in _fkRepositoryUser.Entities
                               .Where(u => u.Id == messages.FromUser).DefaultIfEmpty()
                           from toUser in _fkRepositoryUser.Entities
                               .Where(u => u.Id == messages.ToUser).DefaultIfEmpty()
                           select new MessageDTO
                           {
                               Id = messages.Id,

                               FromUser = messages.FromUser,
                               ToUser = messages.ToUser,

                               UserFrom = fromUser.Name,
                               UserTo = toUser.Name,

                               MessageContent = messages.MessageContent,
                               CreateDate = messages.CreateDate,
                               LastIP = messages.LastIP,
                               Status = messages.Status,
                               IsShow = messages.IsShow,
                           };
                var sql = sql2.AsQueryable();


                //var sql = from fu in _fkRepositoryUser.Entities
                //          join m in _fkRepositoryMessage.Entities
                //               on fu.Id equals m.FromUser into messageGroup
                //          from message in messageGroup.DefaultIfEmpty()
                //          join tu in _fkRepositoryUser.Entities
                //               on message.ToUser equals tu.Id into usergroup
                //          from p in usergroup.DefaultIfEmpty()
                //          .Where(expProducts)
                // ... other joins here

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<MessageDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }


        // ProductBase - ProductCompany 

        public DtResult<ProductBaseDTO> getProductBase(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<ProductBase, bool>> expProducts = c => true;
                //expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryProductBase.Include()
                .Where(expProducts)
                .Select(p => new ProductBaseDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Image = p.Image,
                    RowNum = p.RowNum,
                    CreatedBy = p.CreatedBy,
                    LastUpdatedBy = p.LastUpdatedBy,
                    CreateDate = p.CreateDate,
                    LastUpdateDate = p.LastUpdateDate,
                    LastIP = p.LastIP,
                    Status = p.Status,
                    LangId = p.LangId,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<ProductBaseDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }
        public ProductBase createProductBase(ProductBase pb)
        {
            try
            {

                if (pb.Id > 0)
                {
                    pb.LastUpdateDate = DateTime.Now;
                    pb.LastUpdatedBy = 1;
                    var result = _fkRepositoryProductBase.Update(pb);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    pb.LastUpdateDate = DateTime.Now;
                    pb.LastUpdatedBy = 1;
                    var result = _fkRepositoryProductBase.Add(pb);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool deleteProductBase(ProductBase pb)
        {
            try
            {
                _fkRepositoryProductBase.Delete(pb.Id);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public DtResult<ProductCompanyDTO> getProductCompany(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<ProductCompany, bool>> expProducts = c => true;
                //expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryProductCompany.Include()
                .Where(expProducts)
                .Select(p => new ProductCompanyDTO
                {
                    Id = p.Id,
                    CompanyId = p.CompanyId,
                    CompanyName = p.Company.OfficialName,
                    ProductBaseId = p.ProductBaseId,
                    ProductBaseName = p.ProductBase.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Name = p.Name,
                    Code = p.Code,
                    Price = p.Price,
                    Currency = p.Currency,
                    RowNum = p.RowNum,
                    CreatedBy = p.CreatedBy,
                    LastUpdatedBy = p.LastUpdatedBy,
                    CreateDate = p.CreateDate,
                    LastUpdateDate = p.LastUpdateDate,
                    LastIP = p.LastIP,
                    Status = p.Status,
                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<ProductCompanyDTO>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }
        public ProductCompany createProductCompany(ProductCompany pc)
        {
            try
            {

                if (pc.Id > 0)
                {
                    pc.LastUpdateDate = DateTime.Now;
                    pc.LastUpdatedBy = 1;
                    var result = _fkRepositoryProductCompany.Update(pc);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    pc.CreateDate = DateTime.Now;
                    pc.CreatedBy = 1;
                    var result = _fkRepositoryProductCompany.Add(pc);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool deleteProductCompany(ProductCompany pc)
        {
            try
            {
                _fkRepositoryProductCompany.Delete(pc.Id);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public List<CompanyDTO> getCompanyList(int lang)
        {
            var list = _mapper.Map<List<CompanyDTO>>(_fkRepositoryCompany.Entities.Where(p => p.Status == 1).ToList());
            return list;
        }


        public List<ProductBaseDTO> getProductBaseList(int lang)
        {
            return _mapper.Map<List<ProductBaseDTO>>(_fkRepositoryProductBase.Entities.Where(p => p.Status == 1).ToList());
        }
        public List<CategoryDTO> getCategoryList(int lang)
        {
            return _mapper.Map<List<CategoryDTO>>(_fkRepositoryCategory.Entities.Where(p => p.Status == 1).ToList());
        }



        //StaticContentGrupPage
        public DtResult<StaticContentGrupPageDT> getStaticContentGrupPage(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<StaticContentGrupPage, bool>> expProducts = c => true;

                expProducts = expProducts.And(r => r.LangId == lang);

                // Şablon Filtresi
                int SCGTSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(0));
                if (SCGTSelectListValue != 0)
                {
                    expProducts = expProducts.And(p => p.GrupTempId == SCGTSelectListValue);
                }
                // Başlık Filtresi
                if (!string.IsNullOrEmpty(dtParameters.AdditionalValues.ElementAt(1)))
                {
                    expProducts = expProducts.And(p => p.Title.ToUpper().Contains(dtParameters.AdditionalValues.ElementAt(1).ToUpper())
                                                      || p.Summary.ToUpper().Contains(dtParameters.AdditionalValues.ElementAt(1).ToUpper())
                                                      || p.Html.ToUpper().Contains(dtParameters.AdditionalValues.ElementAt(1).ToUpper()));

                }

                //Resim filtresi
                int PictureSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(4));
                if (PictureSelectListValue == 1)
                {
                    expProducts = expProducts.And(p => !string.IsNullOrEmpty(p.Image1));
                }
                else if (PictureSelectListValue == 0)
                {
                    expProducts = expProducts.And(p => string.IsNullOrEmpty(p.Image1));
                }
                //Video filtresi
                int VideoSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(5));
                if (VideoSelectListValue == 1)
                {
                    expProducts = expProducts.And(p => !string.IsNullOrEmpty(p.Video));
                }
                else if (VideoSelectListValue == 0)
                {
                    expProducts = expProducts.And(p => string.IsNullOrEmpty(p.Video));
                }
                //Yıl min filtresi
                if (!string.IsNullOrEmpty(dtParameters.AdditionalValues.ElementAt(6)))
                {
                    DateTime minYear = Convert.ToDateTime(dtParameters.AdditionalValues.ElementAt(6));
                    expProducts = expProducts.And(r => r.Date >= minYear);
                }
                //Yıl max filtresi
                if (!string.IsNullOrEmpty(dtParameters.AdditionalValues.ElementAt(7)))
                {
                    DateTime maxYear = Convert.ToDateTime(dtParameters.AdditionalValues.ElementAt(7));
                    expProducts = expProducts.And(r => r.Date <= maxYear);
                }

                // Status after CRUD 
                if (dtParameters.AdditionalValues.ElementAt(11) != null)
                {
                    if (Convert.ToString(dtParameters.AdditionalValues.ElementAt(11)) == "true")
                    {
                        expProducts = expProducts.And(p => p.Statu == 1);
                    }
                    else
                    {
                        expProducts = expProducts.And(p => p.Statu != 1);
                    }

                }

                //Status Filtresi
                int StatusSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(8));
                if (StatusSelectListValue == 1)
                {
                    expProducts = expProducts.And(p => p.Statu == 1);
                }
                else if (StatusSelectListValue == 0)
                {
                    expProducts = expProducts.And(p => p.Statu != 1);
                }


                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Title != null && r.Title.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryStaticContentGrupPage.Include(p => p.StaticContentGrupTemp)
                .Where(expProducts)
                .Select(p => new StaticContentGrupPageDT
                {
                    Id = p.Id,
                    GrupTempId = p.GrupTempId,
                    Title = p.Title,
                    Summary = p.Summary,
                    Html = p.Html,
                    OrderNo = p.OrderNo,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    Video = p.Video,
                    SeoKeywords = p.SeoKeywords,
                    SeoDesc = p.SeoDesc,
                    Link = p.Link,
                    Date = p.Date,
                    CreatedDate = p.CreatedDate,
                    ModifiedDate = p.ModifiedDate,
                    Statu = p.Statu,
                    LangId = p.LangId,


                    GrupTemp = p.StaticContentGrupTemp.Name,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<StaticContentGrupPageDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public DtResult<StaticContentGrupPageDT> getStaticContentGrupPageDashboard(DtParameters dtParameters, int lang)
        {
            try
            {
                Expression<Func<StaticContentGrupPage, bool>> expProducts = c => true;
                //expBoats = expBoats.And(p => p.LangId == lang);
                //expBoats = expBoats.And(p => p.Status == 1 || p.Status == 2);              //!Not: pasif olanlar da gözüksün isteniyor



                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Title != null && r.Title.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql2 = from SCGP in _fkRepositoryStaticContentGrupPage.Entities
                           orderby SCGP.ModifiedDate, SCGP.CreatedDate descending
                           select new StaticContentGrupPageDT
                           {
                               Id = SCGP.Id,
                               GrupTempId = SCGP.GrupTempId,
                               Title = SCGP.Title,
                               Summary = SCGP.Summary,
                               Html = SCGP.Html,
                               OrderNo = SCGP.OrderNo,
                               Image1 = SCGP.Image1,
                               Image2 = SCGP.Image2,
                               Image3 = SCGP.Image3,
                               Image4 = SCGP.Image4,
                               Image5 = SCGP.Image5,
                               Video = SCGP.Video,
                               SeoKeywords = SCGP.SeoKeywords,
                               SeoDesc = SCGP.SeoDesc,
                               Link = SCGP.Link,
                               Date = SCGP.Date,
                               CreatedDate = SCGP.CreatedDate,
                               ModifiedDate = SCGP.ModifiedDate,
                               Statu = SCGP.Statu,
                               LangId = SCGP.LangId,


                               GrupTemp = SCGP.StaticContentGrupTemp.Name,
                           };

                var sql = sql2.AsQueryable().Take(5);


                //    /**/
                //}).AsQueryable().Take(5);

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<StaticContentGrupPageDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public StaticContentGrupPage createStaticContentGrupPage(StaticContentGrupPage staticContentGrupPage)
        {
            try
            {

                if (staticContentGrupPage.Id > 0)
                {
                    staticContentGrupPage.ModifiedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentGrupPage.Update(staticContentGrupPage);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    staticContentGrupPage.CreatedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentGrupPage.Add(staticContentGrupPage);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool deleteStaticContentGrupPage(StaticContentGrupPage staticContentGrupPage)
        {
            try
            {
                _fkRepositoryStaticContentGrupPage.Delete(staticContentGrupPage);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        //StaticContentPage
        public DtResult<StaticContentPageDT> getStaticContentPage(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<StaticContentPage, bool>> expProducts = c => true;

                expProducts = expProducts.And(r => r.LangId == lang);

                // Grup Filtresi
                int SCGTSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(0));
                if (SCGTSelectListValue != -1 && SCGTSelectListValue != 0)
                {
                    expProducts = expProducts.And(p => p.GrupId == SCGTSelectListValue);
                }
                // Şablon Filtresi
                int SCTSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(1));
                if (SCTSelectListValue != -1 && SCTSelectListValue != 0)
                {
                    expProducts = expProducts.And(p => p.TempId == SCTSelectListValue);
                }
                // Metin Filtresi
                if (!string.IsNullOrEmpty(dtParameters.AdditionalValues.ElementAt(2)))
                {
                    expProducts = expProducts.And(p => p.Title.ToUpper().Contains(dtParameters.AdditionalValues.ElementAt(2).ToUpper())
                                                        || p.Summary.ToUpper().Contains(dtParameters.AdditionalValues.ElementAt(2).ToUpper())
                                                        || p.Html.ToUpper().Contains(dtParameters.AdditionalValues.ElementAt(2).ToUpper()));
                }

                // Status after CRUD 
                if (dtParameters.AdditionalValues.ElementAt(11) != null)
                {
                    if (Convert.ToString(dtParameters.AdditionalValues.ElementAt(11)) == "true")
                    {
                        expProducts = expProducts.And(p => p.Statu == 1);
                    }
                    else
                    {
                        expProducts = expProducts.And(p => p.Statu != 1);
                    }

                }

                //Status Filtresi
                int StatusSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(5));
                if (StatusSelectListValue == 1)
                {
                    expProducts = expProducts.And(p => p.Statu == 1);
                }
                else if (StatusSelectListValue == 0)
                {
                    expProducts = expProducts.And(p => p.Statu != 1);
                }


                //Yıl min filtresi
                if (!string.IsNullOrEmpty(dtParameters.AdditionalValues.ElementAt(6)))
                {
                    DateTime minYear = Convert.ToDateTime(dtParameters.AdditionalValues.ElementAt(6));
                    expProducts = expProducts.And(r => r.Date >= minYear);
                }
                //Yıl max filtresi
                if (!string.IsNullOrEmpty(dtParameters.AdditionalValues.ElementAt(7)))
                {
                    DateTime maxYear = Convert.ToDateTime(dtParameters.AdditionalValues.ElementAt(7));
                    expProducts = expProducts.And(r => r.Date <= maxYear);
                }
                //Resim filtresi
                int PictureSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(8));
                if (PictureSelectListValue == 1)
                {
                    expProducts = expProducts.And(p => !string.IsNullOrEmpty(p.Image1));
                }
                else if (PictureSelectListValue == 0)
                {
                    expProducts = expProducts.And(p => string.IsNullOrEmpty(p.Image1));
                }
                //Ek filtresi
                int AttachmentSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(9));
                if (AttachmentSelectListValue == 1)
                {
                    expProducts = expProducts.And(p => !string.IsNullOrEmpty(p.Attachment1));
                }
                else if (AttachmentSelectListValue == 0)
                {
                    expProducts = expProducts.And(p => string.IsNullOrEmpty(p.Attachment1));
                }
                //Video filtresi
                int VideoSelectListValue = Convert.ToInt32(dtParameters.AdditionalValues.ElementAt(10));
                if (VideoSelectListValue == 1)
                {
                    expProducts = expProducts.And(p => !string.IsNullOrEmpty(p.Video));
                }
                else if (VideoSelectListValue == 0)
                {
                    expProducts = expProducts.And(p => string.IsNullOrEmpty(p.Video));
                }


                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Title != null && r.Title.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryStaticContentPage.Include(p => p.StaticContentTemp).Include(p => p.StaticContentGrupPage)
                .Where(expProducts)
                .Select(p => new StaticContentPageDT
                {
                    Id = p.Id,
                    GrupId = p.GrupId,
                    TempId = p.TempId,
                    Title = p.Title,
                    Summary = p.Summary,
                    Html = p.Html,
                    OrderNo = p.OrderNo,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Image3 = p.Image3,
                    Image4 = p.Image4,
                    Image5 = p.Image5,
                    Attachment1 = p.Attachment1,
                    Attachment2 = p.Attachment2,
                    Attachment3 = p.Attachment3,
                    Video = p.Video,
                    SeoKeywords = p.SeoKeywords,
                    SeoDesc = p.SeoDesc,
                    Link = p.Link,
                    LangId = p.LangId,
                    Statu = p.Statu,
                    Date = p.Date,
                    CreatedDate = p.CreatedDate,
                    ModifiedDate = p.ModifiedDate,

                    Grup = p.StaticContentGrupPage.Title,
                    Temp = p.StaticContentTemp.Name,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<StaticContentPageDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public DtResult<StaticContentPageDT> getStaticContentPageDashboard(DtParameters dtParameters, int lang)
        {
            try
            {
                Expression<Func<StaticContentPage, bool>> expProducts = c => true;
                //expBoats = expBoats.And(p => p.LangId == lang);
                //expBoats = expBoats.And(p => p.Status == 1 || p.Status == 2);              //!Not: pasif olanlar da gözüksün isteniyor



                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Title != null && r.Title.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql2 = from SCP in _fkRepositoryStaticContentPage.Entities
                           orderby SCP.ModifiedDate, SCP.CreatedDate descending
                           select new StaticContentPageDT
                           {
                               Id = SCP.Id,
                               GrupId = SCP.GrupId,
                               TempId = SCP.TempId,
                               Title = SCP.Title,
                               Summary = SCP.Summary,
                               Html = SCP.Html,
                               OrderNo = SCP.OrderNo,
                               Image1 = SCP.Image1,
                               Image2 = SCP.Image2,
                               Image3 = SCP.Image3,
                               Image4 = SCP.Image4,
                               Image5 = SCP.Image5,
                               Attachment1 = SCP.Attachment1,
                               Attachment2 = SCP.Attachment2,
                               Attachment3 = SCP.Attachment3,
                               Video = SCP.Video,
                               SeoKeywords = SCP.SeoKeywords,
                               SeoDesc = SCP.SeoDesc,
                               Link = SCP.Link,
                               LangId = SCP.LangId,
                               Statu = SCP.Statu,
                               Date = SCP.Date,
                               CreatedDate = SCP.CreatedDate,
                               ModifiedDate = SCP.ModifiedDate,

                               Grup = SCP.StaticContentGrupPage.Title,
                               Temp = SCP.StaticContentTemp.Name,
                           };

                var sql = sql2.AsQueryable().Take(5);


                //    /**/
                //}).AsQueryable().Take(5);

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<StaticContentPageDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public StaticContentPage createStaticContentPage(StaticContentPage staticContentPage)
        {
            try
            {

                if (staticContentPage.Id > 0)
                {
                    staticContentPage.ModifiedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentPage.Update(staticContentPage);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    staticContentPage.CreatedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentPage.Add(staticContentPage);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool deleteStaticContentPage(StaticContentPage staticContentPage)
        {
            try
            {
                _fkRepositoryStaticContentPage.Delete(staticContentPage);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        //StaticContentGrupTemp
        public DtResult<StaticContentGrupTempDT> getStaticContentGrupTemp(DtParameters dtParameters, int lang)
        {
            try
            {
                Expression<Func<StaticContentGrupTemp, bool>> expProducts = c => true;

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Desc != null && r.Desc.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryStaticContentGrupTemp.Include()
                .Where(expProducts)
                .Select(p => new StaticContentGrupTempDT
                {
                    Id = p.Id,
                    Desc = p.Desc,
                    Name = p.Name,
                    CreatedDate = p.CreatedDate,
                    ModifiedDate = p.ModifiedDate,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<StaticContentGrupTempDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public StaticContentGrupTemp createStaticContentGrupTemp(StaticContentGrupTemp staticContentGrupTemp)
        {
            try
            {

                if (staticContentGrupTemp.Id > 0)
                {
                    staticContentGrupTemp.ModifiedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentGrupTemp.Update(staticContentGrupTemp);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    staticContentGrupTemp.CreatedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentGrupTemp.Add(staticContentGrupTemp);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool deleteStaticContentGrupTemp(StaticContentGrupTemp staticContentGrupTemp)
        {
            try
            {
                _fkRepositoryStaticContentGrupTemp.Delete(staticContentGrupTemp);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        //StaticContentTemp
        public DtResult<StaticContentTempDT> getStaticContentTemp(DtParameters dtParameters, int lang)
        {
            try
            {
                Expression<Func<StaticContentTemp, bool>> expProducts = c => true;

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Desc != null && r.Desc.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositoryStaticContentTemp.Include()
                .Where(expProducts)
                .Select(p => new StaticContentTempDT
                {
                    Id = p.Id,
                    Desc = p.Desc,
                    Name = p.Name,
                    CreatedDate = p.CreatedDate,
                    ModifiedDate = p.ModifiedDate,

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<StaticContentTempDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public StaticContentTemp createStaticContentTemp(StaticContentTemp staticContentTemp)
        {
            try
            {

                if (staticContentTemp.Id > 0)
                {
                    staticContentTemp.ModifiedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentTemp.Update(staticContentTemp);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    staticContentTemp.CreatedDate = DateTime.Now;
                    var result = _fkRepositoryStaticContentTemp.Add(staticContentTemp);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool deleteStaticContentTemp(StaticContentTemp staticContentTemp)
        {
            try
            {
                _fkRepositoryStaticContentTemp.Delete(staticContentTemp);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        //Listeler
        public List<StaticContentTemp> listStaticContentTemp()
        {
            var getStaticContentTemp = _fkRepositoryStaticContentTemp.GetList().ToList();
            return getStaticContentTemp;
        }

        public List<StaticContentGrupTemp> listStaticContentGrupTemp()
        {
            var getStaticContentGrupTemp = _fkRepositoryStaticContentGrupTemp.GetList().ToList();
            return getStaticContentGrupTemp;
        }

        public List<StaticContentGrupPage> listStaticContentGrupPage(int lang)
        {
            var getStaticContentGrupPage = _fkRepositoryStaticContentGrupPage.Entities.Where(p => p.LangId == lang && p.Statu == 1).ToList();
            return getStaticContentGrupPage;
        }


        //Slider

        public DtResult<SliderDT> getSlider(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<Slider, bool>> expProducts = c => true;
                expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.Link != null && r.Link.ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                               r.Target != null && r.Target.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositorySlider.Include(p=>p.SliderHeader)
                .Where(expProducts)
                .Select(p => new SliderDT
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image1 = p.Image1,
                    Image2 = p.Image2,
                    Link = p.Link,
                    Target = p.Target,
                    Type = p.Type,
                    Header = p.Header,
                    Text = p.Text,
                    RowNum = p.RowNum,
                    Status = p.Status,
                    LangId = p.LangId,

                    SliderHeaderName = p.SliderHeader.SliderName

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<SliderDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Slider createSlider(Slider pro)
        {
            try
            {

                if (pro.Id > 0)
                {
                    pro.LastUpdatedBy = 1;
                    pro.LastUpdateDate = DateTime.Now;
                    var result = _fkRepositorySlider.Update(pro);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    pro.CreateDate = DateTime.Now;
                    pro.CreatedBy = 1;
                    var result = _fkRepositorySlider.Add(pro);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool deleteSlider(Slider slayt)
        {
            try
            {
                _fkRepositorySlider.Delete(slayt);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        //SliderHeader

        public DtResult<SliderHeaderDT> getSliderHeader(DtParameters dtParameters, int lang)
        {
            try
            {

                Expression<Func<SliderHeader, bool>> expProducts = c => true;
                //expProducts = expProducts.And(p => p.LangId == lang);
                // expProducts = expProducts.And(p => p.Status == 1);

                var searchBy = dtParameters.Search?.Value;
                if (!string.IsNullOrEmpty(searchBy))
                {
                    expProducts = expProducts.And(r => r.SliderName != null && r.SliderName.ToUpper().Contains(searchBy.ToUpper()));
                }

                var sql = _fkRepositorySliderHeader.Include()
                .Where(expProducts)
                .Select(p => new SliderHeaderDT
                {
                    Id = p.Id,
                    SliderName = p.SliderName,
                    CreateDate = p.CreateDate,
                    CreatedBy = p.CreatedBy,
                    Status = p.Status

                    /**/

                }).AsQueryable();

                var orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                var orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "asc";
                sql = orderAscendingDirection ? sql.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : sql.OrderByDynamic(orderCriteria, DtOrderDir.Desc);


                var count = sql.Count();

                var sonuc = sql.Skip(dtParameters.Start).Take(dtParameters.Length).ToList();

                return new DtResult<SliderHeaderDT>
                {
                    Draw = dtParameters.Draw,
                    RecordsTotal = count,
                    RecordsFiltered = count,
                    Data = sonuc
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public SliderHeader createSliderHeader(SliderHeader pro)
        {
            try
            {

                if (pro.Id > 0)
                {
                    var result = _fkRepositorySliderHeader.Update(pro);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    var result = _fkRepositorySliderHeader.Add(pro);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool deleteSliderHeader(SliderHeader sliderHeader)
        {
            try
            {
                _fkRepositorySliderHeader.Delete(sliderHeader);
                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public List<SliderHeader> listSliderHeader(int lang)
        {
            var getSliderHeader = _fkRepositorySliderHeader.Entities.Where(p => p.Status == 1).ToList();
            return getSliderHeader;
        }


    }
}