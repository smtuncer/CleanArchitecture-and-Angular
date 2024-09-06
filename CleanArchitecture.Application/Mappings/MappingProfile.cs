using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBlogCategoryCommand, BlogCategory>().ReverseMap();
        CreateMap<GetAllBlogCategoryQuery, IList<BlogCategory>>().ReverseMap();
        CreateMap<List<BlogCategory>, PaginationResult<BlogCategory>>()
             .ForMember(dest => dest.Datas, opt => opt.MapFrom(src => src))  // List to Datas mapping
             .ForMember(dest => dest.PageNumber, opt => opt.Ignore())         // PageNumber manuel ayarlanacak
             .ForMember(dest => dest.PageSize, opt => opt.Ignore())           // PageSize manuel ayarlanacak
             .ForMember(dest => dest.TotalPages, opt => opt.Ignore())         // TotalPages manuel ayarlanacak
             .ForMember(dest => dest.IsFirstPage, opt => opt.Ignore())        // IsFirstPage manuel ayarlanacak
             .ForMember(dest => dest.IsLastPage, opt => opt.Ignore());
    }
}
