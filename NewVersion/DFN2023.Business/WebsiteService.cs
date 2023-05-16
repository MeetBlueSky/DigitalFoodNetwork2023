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
        IRepository<Message> _fkRepositoryMessage;
        IRepository<Country> _fkRepositoryCountry;

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
            _fkRepositoryMessage = _unitOfWork.GetRepostory<Message>();
            _fkRepositoryCountry = _unitOfWork.GetRepostory<Country>();

            //_fkRepositoryStaticContentPage = _unitOfWork.GetRepostory<StaticContentPage>();
            //_fkRepositoryStaticContentGrupPage = _unitOfWork.GetRepostory<StaticContentGrupPage>();
            //_fkRepositoryUserAccount = _unitOfWork.GetRepostory<UserAccount>();

        }


        public UserDTO CheckUser(string uname, string pass)
        {
            var a = _mapper.Map<UserDTO>(_fkRepositoryUser.Entities.First(p => p.UserName == uname));
            if (a.Status==0)
            {
                if (a.Role==2 && a.EmailConfirmed!=null)
                {
                    return new UserDTO
                    {
                        Id = -1,
                        EmailConfirmed=a.EmailConfirmed,
                    };//Firma Bilgileri girilmedi
                   
                }
                return new UserDTO
                {
                    Id = -2,
                };

            }
            return a;

        }



        public List<CategoryDTO> getCategoryList()
        {
            return _mapper.Map<List<CategoryDTO>>(_fkRepositoryCategory.Entities.Where(p => p.Status == 1).OrderBy(p => p.RowNum).ToList());
        }


        public List<ProductCompanyDTO> getTedarik(int kid, string? urun, int? userid)
        {
            Expression<Func<ProductCompany, bool>> expProductCompany = c => true;
            expProductCompany = expProductCompany.And(p => p.Status == 1);
            if (kid != 0)
            {
                expProductCompany = expProductCompany.And(p => p.CategoryId == kid);
            }
            if (urun != "" && urun!=null)
            {
                expProductCompany = expProductCompany.And(p => p.Name.ToUpper().Contains(urun.ToUpper()));

            }

            var sql = _fkRepositoryProductCompany.Entities
               .Where(expProductCompany)
               .Select(p => new ProductCompanyDTO
               {
                   Id = p.Id,
                   Name = p.Name,
                   CategoryId = p.CategoryId,
                   CategoryName = p.Category.Name,
                   Code = p.Code,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.OfficialName,
                   CreateDate = p.CreateDate,
                   LastUpdateDate = p.LastUpdateDate,
                   CreatedBy = p.CreatedBy,
                   LastUpdatedBy = p.LastUpdatedBy,
                   LastIP = p.LastIP,
                   Currency = p.Currency,
                   Price = p.Price,
                   RowNum = p.RowNum,
                   Status = p.Status,
                   ShortDesc = p.Company.ShortDescription,
                   CityName = _fkRepositoryCity.Entities.Count() > 0 ? _fkRepositoryCity.Entities.First(c => c.Id == p.Company.OfficialCityId).Name:"",
                   CountyName = _fkRepositoryCounty.Entities.Count() > 0 ? _fkRepositoryCounty.Entities.First(c => c.Id == p.Company.OfficialCountyId).Name:"",
                   FavDurum = _fkRepositoryUserUrunler.Entities.Count() > 0 ? _fkRepositoryUserUrunler.Entities.Where(c => c.CompanyId == p.CompanyId && c.UserId == userid).Count() > 0 ? true : false : false,
                   UserId=Convert.ToInt32( p.Company.UserId)
                   //Null kontrollerini unutma

               }).ToList();

            return sql;
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

        public bool favMethod(int companyid, int userid, int durum)
        {
            try
            {

                if (durum > 0)
                {
                    UserUrunler u = _fkRepositoryUserUrunler.Entities.First(x => x.CompanyId == companyid);
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
        public MesajListDT getMesajList(int userid, bool hepsi)
        {
            try
            {
                // Expression<Func<Message, bool>> expMessage = c => true;

                var gonderilenokunmamis = _fkRepositoryMessage.Entities.Where(p => p.Status== 1 && p.ToUser == userid && p.IsShow==false)
                .Select(p => new MessageDTO
                {
                    Id = p.Id,
                    FromUser = p.FromUser,
                    ToUser = p.ToUser,
                    MessageContent=p.MessageContent,
                    CreateDate = p.CreateDate,
                    IsShow=p.IsShow,
                    UserFrom= _fkRepositoryUser.Entities.Count() > 0 ? _fkRepositoryUser.Entities.First(c => c.Id == p.FromUser).UserName : "",

                }).OrderByDescending(p => p.CreateDate).GroupBy(p => p.FromUser).Select(p => p.FirstOrDefault()).ToList();

                var gonderdigimiz = _fkRepositoryMessage.Entities.Where(p => p.Status == 1 && p.FromUser == userid )
                .Select(p => new MessageDTO
                {
                    Id = p.Id,
                    FromUser = p.FromUser,
                    ToUser = p.ToUser,
                    MessageContent = p.MessageContent,
                    CreateDate = p.CreateDate,
                    IsShow = p.IsShow,
                    UserFrom = _fkRepositoryUser.Entities.Count() > 0 ? _fkRepositoryUser.Entities.First(c => c.Id == p.ToUser).UserName : "",

                }).OrderByDescending(p => p.CreateDate).GroupBy(p => p.ToUser).Select(p => p.FirstOrDefault()).ToList();
                MesajListDT a = new MesajListDT();
                a.gelenokunmamis = gonderilenokunmamis;
                for (int i = 0; i < gonderilenokunmamis.Count; i++)
                {
                   if(gonderdigimiz.Select(x => x.ToUser).Contains(gonderilenokunmamis[i].FromUser))
                    {

                   
                        var varolan = gonderdigimiz.Where(p => p.ToUser == gonderilenokunmamis[i].FromUser).ToList();
                        gonderdigimiz.RemoveAll(p => varolan.Contains(p));
                    }

                }

                a.gonderdigimiz = gonderdigimiz;
               
                return a;
            }
            
            catch (Exception e)
            {

                throw;
            }
        }
      

        public List<MessageDTO> getMesajDetay(int userid,int fromid,int rolid,int start,int finish)
        {
            try
            {
                //var mesajlarlist = _fkRepositoryMessage.Entities.Where(p => p.FromUser == fromid && p.ToUser == userid).ToList();
                //for (int i = 0; i < mesajlarlist.Count; i++)
                //{
                //    var mesaj=_fkRepositoryMessage.GetById()
                //}
                List<Message> results = _fkRepositoryMessage.Entities.Where(p => p.FromUser == fromid && p.ToUser == userid).ToList();

                foreach (Message p in results)
                {
                    
                     p.IsShow = true;
                  
                    _fkRepositoryMessage.Update(p);
                    _unitOfWork.SaveChanges();
                }

                
                var sql = _fkRepositoryMessage.Entities
                   .Where(p => p.Status == 1 && (p.ToUser == userid && p.FromUser == fromid) || (p.ToUser == fromid && p.FromUser == userid))
                   .Select(p => new MessageDTO
                   {
                       Id = p.Id,
                       IsShow = p.IsShow,
                       FromUser = p.FromUser,
                       MessageContent = p.MessageContent,
                       ToUser = p.ToUser,
                       CreateDate = p.CreateDate,
                       LastIP = p.LastIP,
                       Status = p.Status,

                   }).AsQueryable();

                if (start!=-1 && finish!=-1)
                {
                    
                   var sonuc = sql.Skip(start).Take(finish).OrderBy(p => p.CreateDate).ToList();
                    return sonuc;
                }
                 return sql.OrderBy(p => p.CreateDate).ToList();
                

            }
            catch (Exception e)
            {

                return null;
            }

        }
    
        public bool mesajYazUser(Message m)
        {
            try
            {

                   _fkRepositoryMessage.Add(m);
                    _unitOfWork.SaveChanges();
                    return true;
                
            }
            catch (Exception)
            {

                return false;
            }
        }
        public string sirketOzelligi(int id,int role)
        {
            string a = "";
            try
            {

                if (role==3)
                {
                    a = _fkRepositoryCompany.Entities.First(p => p.UserId == id).OfficialName;

                }
                else
                {
                    a = _fkRepositoryUser.Entities.First(p => p.Id == id).Name;

                }
                return a;
            }
            catch (Exception e)
            {

                return a;
            }
        }
        public User createUser(User user)
        {
            try
            {
                var a = _fkRepositoryUser.Entities.Where(p => p.UserName == user.UserName || p.Email==user.Email).ToList();

                if (a.Count()>0)
                {
                    return new User
                        {
                            Id = -1,
                        };
                }
                else
                {
                    Random generator = new Random();
                    String r = generator.Next(0, 1000000000).ToString("D9");

                    var body = "Mail Onaylama";
                    var mesaj = false;
                    if (user.Role==2)
                    {
                         mesaj = send(user.Email, "Mail onaylamak için http://localhost:54803/Login/TedMailOnaylama?code=" + r + " linkine tıklayarak onaylayabilirsiniz", body);

                    }
                    else
                    {
                         mesaj = send(user.Email, "Mail onaylamak için http://localhost:54803/Login/TukMailOnaylama?code=" + r + " linkine tıklayarak onaylayabilirsiniz", body);

                    }
                    if (mesaj)
                    {

                        user.EmailConfirmed = r;
                        var result = _fkRepositoryUser.Add(user);
                        _unitOfWork.SaveChanges();
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool send(string pTo, string pBody, string pSubject)
        {
           // var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            MailMessage mm = new MailMessage();
            mm.To.Add(pTo);
            mm.Body = pBody;
            mm.Subject = pSubject;
            mm.IsBodyHtml = true;
            mm.Sender = new MailAddress(
                       "hbeyzakgl@yandex.com"
               );
            mm.From = new MailAddress(
                       "hbeyzakgl@yandex.com"
               );
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Host = "smtp.yandex.com";
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.EnableSsl = true;
                //sc.EnableSsl = Convert.ToBoolean(config["AppSettings:SendMailSMTPSSL"].ToString());
                sc.Port = 587;
                //sc.Port = 587;
                /*sc.Port = Convert.ToInt32(config["AppSettings:SendMailSMTPPort"].ToString()); */// gmail 587

                sc.Credentials = new System.Net.NetworkCredential(
                      "hbeyzakgl@yandex.com.tr",
                         "pera123456."
                );

                sc.Send(mm);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public User kayitKoduKontrol(string code)
        {
            try
            {
                var a = _fkRepositoryUser.Entities.First(p =>p.EmailConfirmed == code);

                a.EmailConfirmDate = DateTime.Now;
               var result= _fkRepositoryUser.Update(a);
                _unitOfWork.SaveChanges();
                return result;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public List<CountryDTO> getCountryList()
        {
            return _mapper.Map<List<CountryDTO>>(_fkRepositoryCountry.Entities.Where(p => p.Status == 1).ToList());
        }


        public List<City> listCities(int id)
        {
            var getCityList = _fkRepositoryCity.GetList(p => p.CountryId == id && p.Status == 1).ToList();
            return getCityList;
        }
        public List<County> listDistrict(int id)
        {
            var getCityList = _fkRepositoryCounty.GetList(p => p.CityId == id && p.Status == 1).ToList();
            return getCityList;
        }

        public Company createFirma(CompanyDTO cm)
        {
            try
            {
                var a = _fkRepositoryCompany.Entities.Where(p => p.UserId == cm.UserId).ToList();

                if (a.Count() > 0)
                {
                    return new Company
                    {
                        Id = -1,
                    };
                }
                else
                {
                     var company = _mapper.Map<Company>(cm);
                    company.TaxNo = "";
                     var result = _fkRepositoryCompany.Add(company);
                     _unitOfWork.SaveChanges();
                     var us = _fkRepositoryUser.Entities.First(p => p.Id==company.UserId);
                     us.Status = 1;
                     _fkRepositoryUser.Update(us);
                    _unitOfWork.SaveChanges();
                    return result;
                   
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}