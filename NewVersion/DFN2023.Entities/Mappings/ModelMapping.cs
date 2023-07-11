using AutoMapper;

using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;

using System;
using System.Collections.Generic;
using System.Text;

namespace DFN2023.Entities.Mappings
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<User, UserDTO>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<User, UserDTO>(); 
            //CreateMap<Company, CompanyDTO>();
            //CreateMap<ProductCompany, ProductCompanyDTO>();

            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();

            CreateMap<CompanyType, CompanyTypeDTO>();
            CreateMap<CompanyTypeDTO, CompanyType>();

            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();
            CreateMap<County, CountyDTO>();
            CreateMap<CountyDTO, Country>();

            CreateMap<MenuManagement, MenuManagementDTO>();
            CreateMap<MenuManagementDTO, MenuManagement>();

            CreateMap<ProductBase, ProductBaseDTO>();
            CreateMap<ProductBaseDTO, ProductBase>();

            CreateMap<ProductCompany, ProductCompanyDTO>();
            CreateMap<ProductCompanyDTO, ProductCompany>(); 
            CreateMap<StaticContentGrupPage, StaticContentGrupPageDTO>();
            CreateMap<Message, MessageDTO>();

            CreateMap<Slider, SliderDTO>();
            CreateMap<SliderDTO, Slider>();
            CreateMap<SliderHeader, SliderHeaderDT>();
            CreateMap<SliderHeaderDT, SliderHeader>();

        }
    }
}
