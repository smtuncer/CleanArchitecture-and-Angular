using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;

public sealed record GetAllBlogCategoryQuery() : IRequest<IList<BlogCategory>>;
