using AutoMapper;
using DFN2023.Contracts;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using DFN2023.Infrastructure.Repositories;
using DFN2023.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IRepository<Message> _fkRepositoryMessage;
        IRepository<ProductBase> _fkRepositoryProductBase;
        IRepository<ProductCompany> _fkRepositoryProductCompany;
        IRepository<Slider> _fkRepositorySlider;
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
            _fkRepositoryMessage = _unitOfWork.GetRepostory<Message>();
            _fkRepositoryProductBase = _unitOfWork.GetRepostory<ProductBase>();
            _fkRepositoryProductCompany = _unitOfWork.GetRepostory<ProductCompany>();
            _fkRepositorySlider = _unitOfWork.GetRepostory<Slider>();
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


    }
}
