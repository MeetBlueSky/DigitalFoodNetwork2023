﻿using AutoMapper;

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
        IRepository<CategoryProductBase> _fkRepositoryCategoryProductBase;
        IRepository<ProductCompany> _fkRepositoryProductCompany;
        IRepository<Company> _fkRepositoryCompany;//StaticContentGrupPageDTO
        IRepository<StaticContentGrupPage> _fkRepositoryStaticContentGrupPage;
        IRepository<City> _fkRepositoryCity;
        IRepository<County> _fkRepositoryCounty;
        IRepository<UserUrunler> _fkRepositoryUserUrunler;
        IRepository<Message> _fkRepositoryMessage;
        IRepository<Country> _fkRepositoryCountry;
        IRepository<CompanyType> _fkRepositoryCompanyType;
        IRepository<ProductBase> _fkRepositoryProductBase;
        IRepository<MenuManagement> _fkRepositoryMenuManagement;

        IMapper _mapper;

        public WebsiteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;


            _fkRepositoryUser = _unitOfWork.GetRepostory<User>();
            _fkRepositoryCategory = _unitOfWork.GetRepostory<Category>();
			_fkRepositoryCategoryProductBase = _unitOfWork.GetRepostory<CategoryProductBase>();
            _fkRepositoryProductCompany = _unitOfWork.GetRepostory<ProductCompany>();
            _fkRepositoryCompany = _unitOfWork.GetRepostory<Company>();
            _fkRepositoryStaticContentGrupPage = _unitOfWork.GetRepostory<StaticContentGrupPage>();
            _fkRepositoryCity = _unitOfWork.GetRepostory<City>();
            _fkRepositoryCounty = _unitOfWork.GetRepostory<County>();
            _fkRepositoryUserUrunler = _unitOfWork.GetRepostory<UserUrunler>();
            _fkRepositoryMessage = _unitOfWork.GetRepostory<Message>();
            _fkRepositoryCountry = _unitOfWork.GetRepostory<Country>();
            _fkRepositoryCompanyType = _unitOfWork.GetRepostory<CompanyType>();
            _fkRepositoryProductBase = _unitOfWork.GetRepostory<ProductBase>();
            _fkRepositoryMenuManagement = _unitOfWork.GetRepostory<MenuManagement>();

            //_fkRepositoryStaticContentPage = _unitOfWork.GetRepostory<StaticContentPage>();
            //_fkRepositoryStaticContentGrupPage = _unitOfWork.GetRepostory<StaticContentGrupPage>();
            //_fkRepositoryUserAccount = _unitOfWork.GetRepostory<UserAccount>();

        }


        public UserDTO CheckUser(string email, string pass)
        {
            var a = _mapper.Map<UserDTO>(_fkRepositoryUser.Entities.First(p => p.Email == email));
            if (a.Status == 0)
            {
                if (a.Role == 2 && a.EmailConfirmed != null)
                {
                    return new UserDTO
                    {
                        Id = -1,
                        EmailConfirmed = a.EmailConfirmed,
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
            try
            {
                return _mapper.Map<List<CategoryDTO>>(_fkRepositoryCategory.Entities.Where(p => p.Status == 1).OrderBy(p => p.RowNum).ToList());

            }
            catch (Exception)
            {

                return null;
            }
        }
        
        public List<ProductBaseDTO> getUstUrunList()
        {
            try
            {
                return _mapper.Map<List<ProductBaseDTO>>(_fkRepositoryProductBase.Entities.Where(p => p.Status == 1).OrderBy(p => p.RowNum).ToList());

            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<CompanyTypeDTO> getCompanyTypeList()
        {
            try
            {
                return _mapper.Map<List<CompanyTypeDTO>>(_fkRepositoryCompanyType.Entities.Where(p => p.Status == 1).OrderBy(p => p.RowNum).ToList());
            }
            catch (Exception e)
            {

                return null;
            }
        }



		public List<CompanyDTO> getTedarik(int kid, string? urun, int? userid)
		{
			Expression<Func<ProductCompany, bool>> expProductCompany = c => true;
			expProductCompany = expProductCompany.And(p => p.Status == 1);
			if (kid != 0)
			{
				var list = _fkRepositoryCategoryProductBase.Include().Include(p => p.ProductBase).ThenInclude(p => p.ProductCompany).ThenInclude(p => p.Company)
			   .Where(p => p.CategoryId == kid)
			   .Select(p => p.ProductBase.Id).ToList();

				expProductCompany = expProductCompany.And(p => list.Contains(p.ProductBaseId));
			}
			if (urun != "" && urun != null)
			{
				expProductCompany = expProductCompany.And(p => p.Name.ToUpper().Contains(urun.ToUpper()));

			}

			List<CompanyDTO> s = new List<CompanyDTO>();


			s = _fkRepositoryProductCompany.Entities.Where(expProductCompany).Select(p => new CompanyDTO
			{
				Id = p.Company.Id,
				BrandName = p.Company.BrandName,
				OfficialName = p.Company.OfficialName,
				ShortDescription = p.Company.ShortDescription,
				DetailDescription = p.Company.DetailDescription,
				TaxOffice = p.Company.TaxOffice,
				TaxNo = p.Company.TaxNo,
				CompanyTypeId = p.Company.CompanyTypeId,
				CompanyTypeName = p.Company.CompanyType.TypeName,
				OfficialAddress = p.Company.OfficialAddress,
				OfficialCityId = p.Company.OfficialCityId,
				OfficialCountryId = p.Company.OfficialCountryId,
				OfficialCountyId = p.Company.OfficialCountyId,
				MapAddress = p.Company.MapAddress,
				MapCountryId = p.Company.MapCountryId,
				MapCityId = p.Company.MapCityId,
				MapCountyId = p.Company.MapCountyId,
				MapX = p.Company.MapX,
				MapY = p.Company.MapY,
				Email = p.Company.Email,
				Phone = p.Company.Phone,
				Mobile = p.Company.Mobile,
				YearFounded = p.Company.YearFounded,
				Logo = p.Company.Logo,
				Attachment = p.Company.Attachment,
				Facebook = p.Company.Facebook,
				Instagram = p.Company.Instagram,
				Tiktok = p.Company.Tiktok,
				Youtube = p.Company.Youtube,
				Whatsapp = p.Company.Whatsapp,
				Website = p.Company.Website,
				AdminNotes = p.Company.AdminNotes,
				CreatedBy = p.CreatedBy,
				LastUpdatedBy = p.LastUpdatedBy,
				CreateDate = p.CreateDate,
				LastUpdateDate = p.LastUpdateDate,
				LastIP = p.LastIP,
				Status = p.Status,
				FavDurum = _fkRepositoryUserUrunler.Entities.Count() > 0 ? _fkRepositoryUserUrunler.Entities.Where(c => c.CompanyId == p.Id && c.UserId == userid).Count() > 0 ? true : false : false,
				CityName = _fkRepositoryCity.Entities.Count() > 0 ? _fkRepositoryCity.Entities.First(c => c.Id == p.Company.OfficialCityId).Name : "",
				CountyName = _fkRepositoryCounty.Entities.Count() > 0 ? _fkRepositoryCounty.Entities.First(c => c.Id == p.Company.OfficialCountyId).Name : "",
				ProductCompany = p.Company.ProductCompany.ToList(),

			}).ToList();
			s = s.DistinctBy(p => p.Id).ToList();


			return s;

		}


		public List<CompanyDTO> getCompanyList()
        {
            List<CompanyDTO> a = new();
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
                var ab = _mapper.Map<List<StaticContentGrupPageDTO>>(_fkRepositoryStaticContentGrupPage.Entities.Where(p => p.GrupTempId == 1 || p.GrupTempId == 3 || p.GrupTempId == 4 || p.GrupTempId == 6).ToList());//.OrderByDescending(p => p.CreateDate).Take(4)
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

        //public MesajListDT getMesajList(int userid, int start,int length)
        //{
        //    try
        //    {
        //        MesajListDT a = new MesajListDT();
        //        Expression<Func<Message, bool>> expMessage = c => true;
        //        expMessage = expMessage.And(p => p.ToUser == userid || p.FromUser==userid);


        //        var sql = _fkRepositoryMessage.Include()
        //        .Where(expMessage)
        //        .Select(p => new MessageDTO
        //        {
        //            Id = p.Id,
        //            FromUser = p.FromUser,
        //            ToUser = p.ToUser,
        //            MessageContent = p.MessageContent,
        //            CreateDate = p.CreateDate,
        //            IsShow = p.IsShow,
        //            UserFrom = _fkRepositoryUser.Entities.Count() > 0 ? _fkRepositoryUser.Entities.First(c => c.Id == p.ToUser).UserName : "",


        //        }).AsQueryable();

        //        sql = sql.OrderByDescending(p => p.CreateDate);
        //        //if (sql.Select(x => new MessageDTO { x.ToUser, x.FromUser }).t)
        //        //{

        //        //}
        //        var gonderilenokunmamis = sql.GroupBy(p => p.FromUser)
        //        .Select(g => g.OrderByDescending(p => p.CreateDate).First())
        //        .ToList();
        //        var gonderdigimiz = sql.GroupBy(p => p.ToUser).Select(p => p.FirstOrDefault()).ToList();

        //        var count = sql.Count();

        //        var sonuc = sql.ToList().Skip(start).Take(length).ToList();
        //        return a;

        //        //return new DtResult<CategoryDTO>
        //        //{
        //        //    RecordsTotal = count,
        //        //    RecordsFiltered = count,
        //        //    Data = sonuc
        //        //};
        //    }

        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}
        public MesajListDT getMesajList(int userid, int start, int length,int role,int entedrol)
        {
            try
            {


                var gelenSonMesajlar  = _fkRepositoryMessage.Entities.Where(p => p.Status == 1 && p.ToUser == userid && p.IsShow == false)
                .Select(p => new MessageDTO
                {
                    Id = p.Id,
                    FromUser = p.FromUser,
                    ToUser = p.ToUser,
                    IsShow = p.IsShow,
                    CreateDate= p.CreateDate,
                    MessageContent = p.MessageContent,  
                    UserFrom = p.User.Name + " " + p.User.Surname
                }).ToList();


                var gelenmesajUserBazliCount = gelenSonMesajlar.Select(p => new MessageDTO
                {
                    FromUser = p.FromUser,
                    ToUser = p.ToUser,
                }).DistinctBy(p => p.FromUser).ToList();


                MesajListDT a = new MesajListDT();
                
                for (int i = 0; i < gelenmesajUserBazliCount.Count; i++)
                {
                    var ml= gelenSonMesajlar.Where(p=>p.FromUser== gelenmesajUserBazliCount[i].FromUser).OrderByDescending(p=>p.CreateDate).FirstOrDefault();
                    a.yenigelenmesaj.Add((MessageDTO)ml);
                     
                }

                a.okunmamiscount = gelenmesajUserBazliCount.Count;
                
                    if (role == entedrol)
                    {

                        var gelenMesajlar = _fkRepositoryMessage.Entities.Where(p => p.Status == 1 && p.ToUser == userid)
                        .Select(p => new MessageDTO
                        {
                            Id = p.Id,
                            FromUser = p.FromUser,
                            ToUser = p.ToUser,
                            IsShow = p.IsShow,
                            CreateDate = p.CreateDate,
                            MessageContent = p.MessageContent,
                            UserFrom = p.User.Name + " " + p.User.Surname
                        }).ToList();

                        var gelenMesajlarCount = gelenMesajlar.Select(p => new MessageDTO
                        {
                            FromUser = p.FromUser,
                            ToUser = p.ToUser,
                        }).DistinctBy(p => p.FromUser).ToList();

                        for (int i = 0; i < gelenMesajlarCount.Count; i++)
                        {
                            var ml = gelenMesajlar.Where(p => p.FromUser == gelenMesajlarCount[i].FromUser).OrderByDescending(p => p.CreateDate).FirstOrDefault();
                            a.mesajlar.Add((MessageDTO)ml);

                        }
                    }
                    else
                    {


                        var gelenMesajlar = _fkRepositoryMessage.Entities.Where(p => p.Status == 1 && (p.FromUser == userid || p.ToUser == userid))
                              .Select(p => new MessageDTO
                              {
                                  Id = p.Id,
                                  FromUser = p.FromUser,
                                  ToUser = p.ToUser,
                                  CreateDate = p.CreateDate,
                                  MessageContent = p.MessageContent,
                                  UserFrom = p.UserT.Name + " " + p.UserT.Surname
                              }).ToList();

                        var gelenMesajlarCount = gelenMesajlar.Where(p=>p.ToUser!=userid).Select(p => new MessageDTO
                        {
                            FromUser = p.FromUser,
                            ToUser=p.ToUser
                        }).DistinctBy(p => p.ToUser).ToList();

                        for (int i = 0; i < gelenMesajlarCount.Count; i++)
                        {
                            var ml = gelenMesajlar.Where(p => p.ToUser == gelenMesajlarCount[i].ToUser).OrderByDescending(p => p.CreateDate).FirstOrDefault();
                            var ml2 = gelenMesajlar.Where(p => p.ToUser == userid && p.FromUser== gelenMesajlarCount[i].ToUser).OrderByDescending(p => p.CreateDate).ToList();
                            if (ml2.Count > 0 && ml2[0].CreateDate > ml.CreateDate)
                            { a.mesajlar.Add((MessageDTO)ml2[0]); }
                            else { a.mesajlar.Add((MessageDTO)ml); }

                        }
                    }

                    for (int i = 0; i < a.yenigelenmesaj.Count; i++)
                    {
                            if (a.mesajlar.Select(x => x.Id).Contains(a.yenigelenmesaj[i].Id))
                            {
                               // var varolan = a.yenigelenmesaj.First(p => p.Id == a.mesajlar[i].Id);
                                a.mesajlar.Remove(a.mesajlar.First(p => p.Id == a.yenigelenmesaj[i].Id));

                            }

                    }
                    a.mesajlar = a.mesajlar.OrderByDescending(p => p.CreateDate).ToList();
                    a.tumcount = a.mesajlar.Count;
                    if (start == -1 && length == -1)
                    {
                        a.mesajlar = new();
                    }
                    else
                    {
                        a.mesajlar = a.mesajlar.Skip(start).Take(length).ToList();
                        a.tumcount = a.tumcount - length < 0 ? 0 : a.tumcount - length;
                    }


                return a;
            }

            catch (Exception e)
            {

                return new MesajListDT();
            }
        }


        public List<MessageDTO> getMesajDetay(int userid, int fromid, int rolid, int start, int finish)
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

                   }).AsQueryable().OrderByDescending(x=>x.CreateDate).ToList();

                if (start != -1 && finish != -1)
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
        public string sirketOzelligi(int id, int role)
        {
            string a = "";
            try
            {

                if (role == 3)
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
                var a = _fkRepositoryUser.Entities.Where(p => p.Email == user.Email).ToList();

                if (a.Count() > 0)
                {
                    return new User
                    {
                        Id = -1,
                        EmailConfirmed = a[0].EmailConfirmed,
                    };
                }
                else
                {
                    String r = Guid.NewGuid().ToString();

                    var body = "Mail Onaylama";
                    var mesaj = false;
                    if (user.Role == 2)
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
                        user.UserName = "";
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
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            MailMessage mm = new MailMessage();
            mm.To.Add(pTo);
            mm.Body = pBody;
            mm.Subject = pSubject;
            mm.IsBodyHtml = true;
            mm.Sender = new MailAddress(
                config["AppSettings:SendMailMessagesFromAddress"].ToString()
               );
            mm.From = new MailAddress(

                config["AppSettings:SendMailMessagesFromAddress"].ToString()
               );
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Host = config["AppSettings:SendMailSMTPHostAddress"].ToString();
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = Convert.ToBoolean(config["AppSettings:SendMailUseDefaultCredentials"]);
                sc.EnableSsl = Convert.ToBoolean(config["AppSettings:SendMailSMTPSSL"]);
                sc.Port = 587;
                sc.Port = Convert.ToInt32(config["AppSettings:SendMailSMTPPort"].ToString());

                sc.Credentials = new System.Net.NetworkCredential(

                config["AppSettings:SendMailSMTPUserName"].ToString(),
                config["AppSettings:SendMailSMTPUserPassword"].ToString()
                );

                sc.Send(mm);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public User kayitKoduKontrol(string code,int rol)
        {
            try
            {
                var a = _fkRepositoryUser.Entities.Where(p => p.EmailConfirmed == code && p.Status!=1).ToList();
                if (a[0].Role!=rol)//Tedarikçi ise
				{
					a[0].Status = 1;
					var result = _fkRepositoryUser.Update(a[0]);
					_unitOfWork.SaveChanges();

				}
				return a[0];

				return a[0];

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
                    a[0].Id = cm.Id;
                    a[0].BrandName = cm.BrandName;
                    a[0].OfficialName = cm.OfficialName;
                    a[0].ShortDescription = cm.ShortDescription;
                    a[0].DetailDescription = cm.DetailDescription;
                    a[0].TaxOffice= cm.TaxOffice;
                    a[0].TaxNo = cm.TaxNo;
                    a[0].CompanyTypeId = cm.CompanyTypeId;
                    a[0].OfficialAddress = cm.OfficialAddress;
                    a[0].MapCountryId = cm.MapCountryId;
                    a[0].MapCityId = cm.MapCityId;
                    a[0].MapCountyId = cm.MapCountyId;
                    a[0].MapAddress = cm.MapAddress;
                    a[0].MapCountryId=cm.MapCountryId;
                    a[0].MapCityId=cm.MapCityId;
                    a[0].MapCountyId=cm.MapCountyId;
                    a[0].MapX = cm.MapX;
                    a[0].MapY=cm.MapY;
                    a[0].Email = cm.Email;
                    a[0].Phone = cm.Phone;
                    a[0].Mobile = cm.Mobile;
                    a[0].YearFounded = cm.YearFounded;
					a[0].Logo = cm.Logo==null? a[0].Logo: cm.Logo;
                    a[0].Attachment = cm.Attachment==null? a[0].Attachment: cm.Attachment;
                    a[0].Facebook = cm.Facebook;
                    a[0].Instagram = cm.Instagram;
                    a[0].Tiktok = cm.Tiktok;
                    a[0].Youtube = cm.Youtube;
                    a[0].Whatsapp = cm.Whatsapp;
                    a[0].Website = cm.Website;
                    a[0].AdminNotes = cm.AdminNotes;
                    a[0].CreateDate = cm.CreateDate;
                    a[0].CreatedBy = cm.CreatedBy;
                    a[0].LastIP = cm.LastIP;
                    a[0].LastUpdateDate = cm.LastUpdateDate;
                    a[0].LastUpdatedBy = cm.LastUpdatedBy;
                    a[0].Status = cm.Status;
                    a[0].UserId = cm.UserId;
						
					var result = _fkRepositoryCompany.Update(a[0]);
					_unitOfWork.SaveChanges();

					return result;
                }
                else
                {


					var company = _mapper.Map<Company>(cm);


					var result = _fkRepositoryCompany.Add(company);
					_unitOfWork.SaveChanges();
					
                    var us = _fkRepositoryUser.Entities.Where(p => p.Id == company.UserId).ToList();
                    if (us.Count > 0)
                    {
                        us[0].EmailConfirmDate = DateTime.Now;
                        us[0].Status = 1;
                        _fkRepositoryUser.Update(us[0]);
                        _unitOfWork.SaveChanges();
					}
					return result;

				}
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ProductCompanyDTO> getUrunlerList(int sirketid)
        {
            try
            {
                
                    var sql = _fkRepositoryProductCompany.Entities.Include(p => p.Company).Include(p => p.Category).Where(p => p.Status == 1 && p.CompanyId == sirketid)
                                    .Select(p => new ProductCompanyDTO
                                    {
                                        Id = p.Id,
                                        CategoryId = p.CategoryId,
                                        Name = p.Name,
                                        UserId = Convert.ToInt32(p.Company.UserId),
                                        CategoryName = p.Category.Name,
                                        Code = p.Code,
                                        CompanyId = p.CompanyId,
                                        UnitId = p.UnitId,
                                       // Unit = p.UnitId == 1 ? "Gr" : p.UnitId == 2 ? "Kg" : "Adet",
                                        Price = p.Price,
                                        Desc = p.Desc,
                                        Currency = p.Currency==null?0: p.Currency,  
                                    }).ToList();


                    return sql;
                
                
            }

            catch (Exception e)
            {

                return null;
            }
        }

        public ProductCompany createUrun(ProductCompanyDTO comp)
        {
            try
            {

                var company = _mapper.Map<ProductCompany>(comp);
                if (company.Id > 0)
                {
                    var a = _fkRepositoryProductCompany.Entities.First(x => x.Id == company.Id);
                    a.CategoryId = company.CategoryId;
                    a.Name = company.Name;
                    a.Price = company.Price;
                    a.UnitId = company.UnitId;
                    a.Desc = company.Desc;
                    var result = _fkRepositoryProductCompany.Update(a);
                    _unitOfWork.SaveChanges();
                    return result;
                }
                else
                {
                    var result = _fkRepositoryProductCompany.Add(company);
                    _unitOfWork.SaveChanges();
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool deleteUrun(int id)
        {
            try
            {
                ProductCompany c = _fkRepositoryProductCompany.GetById(id);
                c.Status = 0;
                _fkRepositoryProductCompany.Update(c);
                _unitOfWork.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;

            }
        }
        public int getCompanyId(int userid)
        {
            try
            {

                var a = _fkRepositoryCompany.Entities.Where(x => x.UserId == userid).ToList();
                if (a.Count>0)
                {
                    int dgr = a[0].Id;
                    return dgr;
                }

                return 0;

            }
            catch (Exception)
            {
                return 0;


            }
        }
        public User sifreYenile(string email,string code)
        {
            try
            {
                var a = _fkRepositoryUser.Entities.Where(p => p.Email == email).ToList();
                if (a.Count > 0)
                {
                    a[0].Password = code;
                    var result = _fkRepositoryUser.Update(a[0]);
                    _unitOfWork.SaveChanges();
                    return a[0];
                }
                return new User
                {
                    Id = -1,
                };
            }
            catch (Exception)
            {

                return null;
            }
           
        }


        public CompanyDTO getCompanyDetay(int id,int userid)
        {
            try
            {

				var a = _mapper.Map<List<CompanyDTO>>(_fkRepositoryCompany.Entities.Include(p => p.CompanyImage).Where(x => x.Id == id).ToList());
				if (a.Count > 0)
                {
                    a[0].Fav=_fkRepositoryUserUrunler.Entities.Where(x => x.CompanyId == a[0].Id && x.UserId==userid).ToList().Count>0?true:false;
					return a[0];
                }

                return null;

            }
            catch (Exception)
            {
                return null;


            }
        }

		public CompanyDTO getCompanyInfo(int userid)
		{
			try
			{
				var a = _mapper.Map<List<CompanyDTO>>(_fkRepositoryCompany.Entities.Where(p => p.UserId == userid && p.Status == 1).ToList());
				
				return a[0];

			}
			catch (Exception)
			{

				return null;
			}

		}

        public List<MenuManagementDTO> getMenuLayer1(int lang)
        {
            try
            {

                var result = _mapper.Map<List<MenuManagementDTO>>(_fkRepositoryMenuManagement.Entities.Where(p => p.Status == 1 && p.MenuLayer == 1 && p.LangId == lang).OrderBy(p => p.RowNum).ToList());

                for (int i = 0; i < result.Count; i++)
                {
                    result[i].Layer2 = _mapper.Map<List<MenuManagementDTO>>(_fkRepositoryMenuManagement.Entities.Where(p => p.Status == 1 && p.MenuLayer == 2 && p.MenuLayerCode == result[i].MenuLayerCode && p.LangId == lang).OrderBy(p => p.RowNum).ToList());
                    for (int j = 0; j < result[i].Layer2.Count; j++)
                    {
                        result[i].Layer2[j].Layer3 = _mapper.Map<List<MenuManagementDTO>>(_fkRepositoryMenuManagement.Entities.Where(p => p.Status == 1 && p.MenuLayer == 3 && p.MenuLayerCode == result[i].MenuLayerCode && result[i].Layer2[j].Id == p.ParentId && p.LangId == lang).OrderBy(p => p.RowNum).ToList());
                        for (int k = 0; k < result[i].Layer2[j].Layer3.Count; k++)
                        {
                            result[i].Layer2[j].Layer3[k].Layer4 = _mapper.Map<List<MenuManagementDTO>>(_fkRepositoryMenuManagement.Entities.Where(p => p.Status == 1 && p.MenuLayer == 4 && p.MenuLayerCode == result[i].MenuLayerCode && result[i].Layer2[j].Id == p.ParentId && result[i].Layer2[j].Layer3[k].Id == p.ChildId && p.LangId == lang).OrderBy(p => p.RowNum).ToList());

                        }
                    }


                }



                return result;
            }
            catch (Exception)
            {

                return new();
            }
        }



    }
}