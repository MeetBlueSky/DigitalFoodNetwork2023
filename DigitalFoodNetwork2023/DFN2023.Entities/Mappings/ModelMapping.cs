using AutoMapper;
using DFN2023.Entities.DTO;
using DFN2023.Entities.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace DFN2023.Entities.Mappings
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();
            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();
            CreateMap<UserDTO, User>();
            //CreateMap<LoginInfo, User>();
            //CreateMap<User, LoginInfo>();
            //CreateMap<Segment, SegmentDTO>();
            //CreateMap<SegmentDTO,Segment>(); 

            //CreateMap<SegmentDescDTO, SegmentDesc>(); 
            //CreateMap<SegmentDesc, SegmentDescDTO>(); 

            //CreateMap<Blog, BlogDTO>();
            //CreateMap<BlogDTO, Blog>();
            //CreateMap<BlogDTO, Blog>();
            //CreateMap<CurrencyDTO, Currency>();
            //CreateMap<Currency, CurrencyDTO>();
            //CreateMap<Brands, BrandsDTO>();
            //CreateMap<StaticContentPageDTO, StaticContentPage>();
            //CreateMap<StaticContentPage, StaticContentPageDTO>();

            //CreateMap<StaticContentNewsDTO, StaticContentPage>();
            //CreateMap<StaticContentPage,StaticContentNewsDTO>().ForMember(d => d.TitleTR, x => x.Ignore()).ForMember(d => d.SummaryTR, x => x.Ignore()).ForMember(d => d.HtmlTR, x => x.Ignore()).ForMember(d => d.SeoKeywordsTR, x => x.Ignore()).ForMember(d => d.KeywordsTR, x => x.Ignore()).ForMember(d => d.SeoDescTR, x => x.Ignore()).ForMember(d => d.TitleEN, x => x.Ignore()).ForMember(d => d.SummaryEN, x => x.Ignore()).ForMember(d => d.HtmlEN, x => x.Ignore()).ForMember(d => d.SeoKeywordsEN, x => x.Ignore()).ForMember(d => d.KeywordsEN, x => x.Ignore()).ForMember(d => d.SeoDescEN, x => x.Ignore()); 

            //CreateMap<StaticContentGrupPage, StaticContentGrupPageDT>();
            //CreateMap<StaticContentGrupPageDT, StaticContentGrupPage>();

            //CreateMap<BrandsDTO, Brands>(); 
            //CreateMap<BrandModel, BrandsDTO>();

            //CreateMap<BrandsDTO, BrandDesc>();
            //CreateMap<BrandDesc, BrandsDTO>().ForMember(d => d.CreatetDate, x => x.Ignore()).ForMember(d => d.LastUpdateDate, x => x.Ignore()).ForMember(d => d.Status, x => x.Ignore()).ForMember(d => d.ShowInHome, x => x.Ignore()).ForMember(d => d.ByTrio, x => x.Ignore()).ForMember(d => d.CreatedBy, x => x.Ignore()).ForMember(d => d.GrupPageId, x => x.Ignore()).ForMember(d => d.Id, x => x.Ignore()).ForMember(d => d.Image, x => x.Ignore()).ForMember(d => d.Image2, x => x.Ignore()).ForMember(d => d.LastUpdateBy, x => x.Ignore()).ForMember(d => d.RowNum, x => x.Ignore());

            ////CreateMap<BlogTemplateDTO, BlogTemplate>().ForMember(d => d.Blog, x => x.Ignore());
            //CreateMap<Boats, BoatsDTO>();
            //CreateMap<BoatsDTO, Boats>().ForMember(d => d.StokTypes, x => x.Ignore()).ForMember(d => d.Brands, x => x.Ignore()).ForMember(d => d.ModelGroup, x => x.Ignore());

            //CreateMap<ModelGroup, ModelGroupDTO>();
            //CreateMap<ModelGroupDTO, ModelGroup>().ForMember(d => d.Brands, x => x.Ignore()).ForMember(d => d.SubCategoryBrand, x => x.Ignore());
            //CreateMap<ModelBrandC, ModelGroupDTO>();

            //CreateMap<SalesPeople, SalesPeopleDTO>();
            //CreateMap<SalesPeopleDTO, SalesPeople>();

            //CreateMap<SalesBrandPeople, SalesBrandPeopleDTO>();
            //CreateMap<SalesBrandPeopleDTO, SalesBrandPeople>().ForMember(d => d.Brands, x => x.Ignore()).ForMember(d => d.SalesPeople, x => x.Ignore());

            //CreateMap<BoatDoc, BoatDocDTO>();
            //CreateMap<BoatDocDTO, BoatDoc>();

            //CreateMap<BoatText, BoatTextDTO>();
            //CreateMap<BoatTextDTO, BoatText>();

            //CreateMap<BoatVideo, BoatVideoDTO>();
            //CreateMap<BoatVideoDTO, BoatVideo>();

            //CreateMap<BoatImage, BoatImageDTO>();
            //CreateMap<BoatImageDTO, BoatImage>();

            //CreateMap<StokTypes, StockTypeDTO>();
            //CreateMap<StockTypeDTO, StokTypes>();


            //CreateMap<CommunicationForm, CommunicationDTO>();
            //CreateMap<CommunicationDTO, CommunicationForm>();

            //CreateMap<CampaingLink, CampaignLinkDTO>();
            //CreateMap<CampaignLinkDTO, CampaingLink>();

            //CreateMap<CampaignForm, CampaignFormDTO>();
            //CreateMap<CampaignFormDTO, CampaignForm>();

            //CreateMap<CampaignLog, CampaignLogDTO>();
            //CreateMap<CampaignLogDTO, CampaignLog>();

            //CreateMap<Campaign, CampaignDTO>();
            //CreateMap<CampaignDTO, Campaign>();


            //CreateMap<MenuManagement, MenuManagementDTO>();
            //CreateMap<MenuManagementDTO, MenuManagement>();

            //CreateMap<LetsCallYou, LetsCallYouDTO>();
            //CreateMap<LetsCallYouDTO, LetsCallYou>();
            //CreateMap<SubCategoryBrand, SubCategoryBrandDTO>();
            //CreateMap<SubCategoryBrandDTO, SubCategoryBrand>();

            //CreateMap<BoatsModelC, Boats>();
            //CreateMap<Boats, BoatsModelC>();
            //CreateMap<BoatsModelC, BoatsDTO>();
            //CreateMap<BoatsDTO, BoatsModelC>();
            //CreateMap<BoatsModelC, BoatModelC>();
            //CreateMap<BoatModelC, BoatsModelC>();
            //CreateMap<BoatImageDTO, BoatsImagesC>();
            //CreateMap<BoatsImagesC, BoatImageDTO>();
            //CreateMap<BoatImage, BoatsImages>();
            //CreateMap<BoatsImages, BoatImage>();
            //CreateMap<BoatsVideos, BoatVideo>();
            //CreateMap<BoatVideo, BoatsVideos>();
            //CreateMap<BoatsVideosC, BoatVideoDTO>();
            //CreateMap<BoatVideoDTO, BoatsVideosC>();
            //CreateMap<BoatAttachmentC, BoatDocDTO>();
            //CreateMap<BoatDocDTO, BoatAttachmentC>();
            //CreateMap<BoatAttachment, BoatDoc >();
            //CreateMap<BoatDoc, BoatAttachment>();

            //CreateMap<SalespeopleC, SalesPeople>();
            //CreateMap<SalesPeople, SalespeopleC>();


        }
    }
}
