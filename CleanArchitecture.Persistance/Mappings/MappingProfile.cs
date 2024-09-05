﻿using AutoMapper;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Persistance.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBlogCategoryCommand, BlogCategory>().ReverseMap();
        CreateMap<GetAllBlogCategoryQuery, BlogCategory>().ReverseMap();
    }
}
