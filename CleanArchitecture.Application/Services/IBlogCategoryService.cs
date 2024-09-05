﻿using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;
public interface IBlogCategoryService
{
    Task CreateAsync(CreateBlogCategoryCommand request, CancellationToken cancellationToken);
    Task<IList<BlogCategory>> GetAllAsync(GetAllBlogCategoryQuery request, CancellationToken cancellationToken);
}
