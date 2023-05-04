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

            CreateMap<ProductBase, ProductBaseDTO>();
            CreateMap<ProductBaseDTO, ProductBase>();

            CreateMap<ProductCompany, ProductCompanyDTO>();
            CreateMap<ProductCompanyDTO, ProductCompany>(); 
            CreateMap<StaticContentGrupPage, StaticContentGrupPageDTO>();
        }
    }
}
