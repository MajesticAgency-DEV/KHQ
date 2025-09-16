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
            
            CreateMap<Brouchures, BrouchuresDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.TitleAr : src.TitleEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.DescriptionAr : src.DescriptionEn));

            CreateMap<StainDetails, StainDetailsDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
    CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar"
        ? (src.TitleAr ?? src.TitleEn)
        : (src.TitleEn ?? src.TitleAr)));

            CreateMap<StainDetailsDto, StainDetails>();


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

            CreateMap<E_Con_Inner, E_Con_InnerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.NameAr : src.NameEn));

            CreateMap<Stains, StainsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.NameAr : src.NameEn));

            CreateMap<H_AboutUs, H_AboutUsDto>()
                .ForMember(dest => dest.Point, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.PointAr : src.PointEn));

            CreateMap<StainDetails, StainDetailsDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.TitleAr : src.TitleEn))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                 CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? src.DescriptionAr : src.DescriptionEn));

            CreateMap<BaseHome, BaseHomeVM>().ReverseMap();
            CreateMap<BaseHomeVM, BaseHome>().ReverseMap();

            CreateMap<Brouchures, BrouchuresVM>().ReverseMap();
            CreateMap<BrouchuresVM, Brouchures>().ReverseMap();

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

            CreateMap<SubProduct, SubProductVM>().ReverseMap();
            CreateMap<SubProductVM, SubProduct>().ReverseMap();

            CreateMap<FAQ, FaqVM>().ReverseMap();
            CreateMap<FaqVM, FAQ>().ReverseMap();

            CreateMap<E_Con_Inner, E_Con_InnerVM>().ReverseMap();
            CreateMap<E_Con_InnerVM, E_Con_Inner>().ReverseMap();

            CreateMap<ContactUs, ContactUsVM>().ReverseMap();
            CreateMap<ContactUsVM, ContactUs>().ReverseMap();

            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<Slider, SliderVM>().ReverseMap();
            CreateMap<SliderVM, Slider>().ReverseMap();

            CreateMap<H_AboutUs, H_AboutUsVM>().ReverseMap();
            CreateMap<H_AboutUsVM, H_AboutUs>().ReverseMap();

            CreateMap<SocialMediaVM, SocialMedia>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaVM>().ReverseMap();

            CreateMap<SocialMediaDto, SocialMedia>().ReverseMap();
            CreateMap<SocialMedia, SocialMediaDto>().ReverseMap();

            CreateMap<StainsDto, Stains>().ReverseMap();
            CreateMap<Stains, StainsDto>().ReverseMap();

            CreateMap<StainDetailsDto, StainDetails>().ReverseMap();
            CreateMap<StainDetails, StainDetailsDto>().ReverseMap();

            CreateMap<EmailsDto, Emails>().ReverseMap();
            CreateMap<Emails, EmailsDto>().ReverseMap();

            CreateMap<EmailsVM, Emails>().ReverseMap();
            CreateMap<Emails, EmailsVM>().ReverseMap();
        }
    }

}
