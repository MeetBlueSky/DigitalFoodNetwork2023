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

        //IRepository<Slayt> _fkRepositorySlayt;
        ////IRepository<ContactForm> _fkRepositoryContactForm;
        //IRepository<StaticContentPage> _fkRepositoryStaticContentPage;
        //IRepository<StaticContentGrupPage> _fkRepositoryStaticContentGrupPage;
        //IRepository<UserAccount> _fkRepositoryUserAccount;

        IMapper _mapper;

        public WebsiteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;


            //_fkRepositorySlayt = _unitOfWork.GetRepostory<Slayt>();

            //_fkRepositoryStaticContentPage = _unitOfWork.GetRepostory<StaticContentPage>();
            //_fkRepositoryStaticContentGrupPage = _unitOfWork.GetRepostory<StaticContentGrupPage>();
            //_fkRepositoryUserAccount = _unitOfWork.GetRepostory<UserAccount>();

        }

        //public List<SlaytDTO> getSlaytForHome(int langId)
        //{
        //    return _mapper.Map<List<SlaytDTO>>(_fkRepositorySlayt.Entities.Where(p => p.LangId == langId && p.Status == 1 && p.Type == 1).OrderBy(p => p.RowNum).ToList());
        //}

        //public StaticContentGrupPageDTO getStaticGrup(int pid, int lang)
        //{



        //    // List<StaticContentGrupPage> result2 = _fkRepositoryStaticContentGrupPage.Include(p => p.StaticContentPage).Where(p => p.Id == pid && p.LangId == lang).ToList();
        //    // StaticContentGrupPage result = result2.FirstOrDefault();

        //    var result = _fkRepositoryStaticContentGrupPage.Include(p => p.StaticContentPage).Where(p => p.Id == pid && p.LangId == lang).FirstOrDefault();
        //        // FirstOrDefault(p => p.Id == pid && p.LangId == lang);

        //    //  _mapper.Map< StaticContentGrupPageDTO>();

        //    var sonuc = _mapper.Map<StaticContentGrupPageDTO>(result);

        //    if (sonuc != null)
        //    {
        //        sonuc.StaticContentPage = _mapper.Map<List<StaticContentPageDTO>>(result.StaticContentPage.ToList());
        //    }

        //    return sonuc;
        //}




        //public List<StaticContentPageDTO> getStaticContentPageList(int langId, int start, int grupID, int adet)
        //{
        //    var sql = _fkRepositoryStaticContentPage.Entities.Where(p => p.Statu == 1 && p.LangId == langId && p.GrupId == grupID);
        //    int count = sql.Count();
        //    var result = _mapper.Map<List<StaticContentPageDTO>>(sql.OrderByDescending(p => p.Id).Skip(start * adet).Take(adet).ToList());//.OrderByDescending(p => p.Date)
        //    if (result.Count > 0) { result[0].Count = count; }

        //    return result;
        //}

        //public List<StaticContentPageDTO> getStaticPageListByTempId(int tempID, int langId)
        //{
        //    var sql = _fkRepositoryStaticContentPage.Entities.Where(p => p.Statu == 1 && p.LangId == langId && p.TempId == tempID);
        //    int count = sql.Count();
        //    var result = _mapper.Map<List<StaticContentPageDTO>>(sql.OrderByDescending(p => p.Date).ToList());
        //    if (result.Count > 0) { result[0].Count = count; }

        //    return result;
        //}

        //public List<StaticContentPageDTO> getStaticContentPageListMultiple(int langId, int start, int grupID, int grupID2, int adet)
        //{
        //    Expression<Func<StaticContentPage, bool>> expStaticContents = c => true;
        //    expStaticContents = expStaticContents.And(p => p.Statu == 1); 
        //    expStaticContents = expStaticContents.And(p => p.GrupId == grupID || p.GrupId == grupID2);

        //    var sql = _fkRepositoryStaticContentPage.Entities.Where(expStaticContents).OrderBy(p=> p.Date);
        //    int count = sql.Count();
        //    var result = _mapper.Map<List<StaticContentPageDTO>>(sql.OrderByDescending(p => p.Id).Skip(start * adet).Take(adet).ToList());//.OrderByDescending(p => p.Date)
        //    if (result.Count > 0) { result[0].Count = count; }

        //    return result;
        //}

        //public StaticContentPageDTO getStaticPage(int pid, int lang)
        //{

        //    var result = _mapper.Map<StaticContentPageDTO>(_fkRepositoryStaticContentPage.Entities.FirstOrDefault(p => p.Id == pid && p.LangId == lang && p.Statu == 1));


        //    return result;
        //}

        //public List<StaticContentGrupPageDTO> getStaticContentPublications()
        //{

        //    var result = _mapper.Map<List<StaticContentGrupPageDTO>>(_fkRepositoryStaticContentGrupPage.Entities.Where(p => p.GrupTempId == 12 && p.Id != 12 && p.Statu == 1).Include(p=>p.StaticContentPage.Where(p=>p.Statu == 1).OrderBy(p=> p.OrderNo)).OrderByDescending(p=>p.Id).ToList());

        //    return result;
        //}
    }
}
