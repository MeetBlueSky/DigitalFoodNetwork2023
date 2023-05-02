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
            //CreateMap<UserAccount, UserAccountDTO>();
            //CreateMap<Slayt, SlaytDTO>();

            //CreateMap<StaticContentGrupPage, StaticContentGrupPageDTO>();
            //CreateMap<StaticContentPage, StaticContentPageDTO>();

            //CreateMap<MenuManagement, MenuManagementDTO>();
            //CreateMap<MenuManagementDTO, MenuManagement>();

            //CreateMap<Language, LanguageDTO>();
            //CreateMap<LanguageDTO, Language>();

            //CreateMap<Login, LoginDTO>();
            //CreateMap<LoginDTO, Login>();
        }
    }
}
