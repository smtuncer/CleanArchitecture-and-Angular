using AutoMapper;
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
    }
}
