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

            CreateMap<Language, LanguageDTO>();
            CreateMap<LanguageDTO, Language>();

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

			// **   DeepBlue Mappings for Content

			// CreateMap<CommunicationForm, CommunicationFormDTO>();
			// CreateMap<CommunicationFormDTO, CommunicationForm>();

			CreateMap<User, UserDTO>();
			// CreateMap<UserAccount, UserAccountDTO>();
			// CreateMap<Slayt, SlaytDTO>();

			CreateMap<StaticContentGrupPage, StaticContentGrupPageDTO>();
			CreateMap<StaticContentPage, StaticContentPageDTO>();

			// CreateMap<NewsLetter, NewsLetterDTO>();
			// CreateMap<NewsLetterDTO, NewsLetter>();

			CreateMap<MenuManagement, MenuManagementDTO>();
			CreateMap<MenuManagementDTO, MenuManagement>();

			CreateMap<Language, LanguageDTO>();
			CreateMap<LanguageDTO, Language>();

			// CreateMap<Login, LoginDTO>();
			// CreateMap<LoginDTO, Login>();

			// CreateMap<Slayt, SlaytDTO>();
			// CreateMap<SlaytDTO, Slayt>();
			CreateMap<SliderHeader, SliderHeaderDT>();
			CreateMap<SliderHeaderDT, SliderHeader>();

		}
    }
}
