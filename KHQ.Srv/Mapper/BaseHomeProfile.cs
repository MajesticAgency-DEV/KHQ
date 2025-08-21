using AutoMapper;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using System.Globalization;

namespace KHQ.Srv.Mapper
{
    public class BaseHomeProfile : Profile
    {
        public BaseHomeProfile()
        {
            CreateMap<BaseHome, BaseHomeDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.TitleAr : src.TitleEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.DescriptionAr : src.DescriptionEn));
            
            CreateMap<AboutUs, AboutUsDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.TitleAr : src.TitleEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.DescriptionAr : src.DescriptionEn));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.NameAr : src.NameEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.DescriptionAr : src.DescriptionEn));

            CreateMap<Brands, BrandsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.NameAr : src.NameEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.DescriptionAr : src.DescriptionEn));

            //CreateMap<E_Con_Inner, E_Con_InnerDto>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
            //     CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.NameAr : src.NameEn));

            CreateMap<Stains, StainsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.NameAr : src.NameEn));

            CreateMap<StainDetails, StainDetailsDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.TitleAr : src.TitleEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.DescriptionAr : src.DescriptionEn));

            CreateMap<BaseHome, BaseHomeVM>().ReverseMap();
            CreateMap<BaseHomeVM, BaseHome>().ReverseMap();

            CreateMap<AboutUs, AboutUsVM>().ReverseMap();
            CreateMap<AboutUsVM, AboutUs>().ReverseMap();

            CreateMap<Stains, StainsVM>().ReverseMap();
            CreateMap<StainsVM, Stains>().ReverseMap();

            CreateMap<StainDetails, StainDetailsVM>().ReverseMap();
            CreateMap<StainDetailsVM, StainDetails>().ReverseMap();

            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<CategoryVM, Category>().ReverseMap();

            CreateMap<Brands, BrandsVM>().ReverseMap();
            CreateMap<BrandsVM, Brands>().ReverseMap();

            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<ProductVM, Product>().ReverseMap();

            CreateMap<FAQ, FaqVM>().ReverseMap();
            CreateMap<FaqVM, FAQ>().ReverseMap();

            CreateMap<E_Con_Inner, E_Con_InnerVM>().ReverseMap();
            CreateMap<E_Con_InnerVM, E_Con_Inner>().ReverseMap();

            CreateMap<ContactUs, ContactUsVM>().ReverseMap();
            CreateMap<ContactUsVM, ContactUs>().ReverseMap();

            CreateMap<Slider, SliderVM>().ReverseMap();
            CreateMap<SliderVM, Slider>().ReverseMap();

            CreateMap<SocialMediaVM, SocialMedia>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaVM>().ReverseMap();

            CreateMap<SocialMediaDto, SocialMedia>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaDto>().ReverseMap();
        }
    }

}
