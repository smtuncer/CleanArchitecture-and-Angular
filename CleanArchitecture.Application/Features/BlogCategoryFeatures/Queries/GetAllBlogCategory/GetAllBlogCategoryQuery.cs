using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;

public sealed record GetAllBlogCategoryQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string Search = "") : IRequest<PaginationResult<BlogCategory>>;
