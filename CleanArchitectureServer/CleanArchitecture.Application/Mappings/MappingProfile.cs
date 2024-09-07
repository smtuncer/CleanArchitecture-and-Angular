using AutoMapper;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.UpdateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBlogCategoryCommand, BlogCategory>().ReverseMap();
        CreateMap<UpdateBlogCategoryCommand, BlogCategory>().ReverseMap();
        CreateMap<GetAllBlogCategoryQuery, IList<BlogCategory>>().ReverseMap();
        CreateMap<List<BlogCategory>, PaginationResult<BlogCategory>>()
             .ForMember(dest => dest.Datas, opt => opt.MapFrom(src => src))
             .ForMember(dest => dest.PageNumber, opt => opt.Ignore())
             .ForMember(dest => dest.PageSize, opt => opt.Ignore())
             .ForMember(dest => dest.TotalPages, opt => opt.Ignore())
             .ForMember(dest => dest.IsFirstPage, opt => opt.Ignore())
             .ForMember(dest => dest.IsLastPage, opt => opt.Ignore());
    }
}
