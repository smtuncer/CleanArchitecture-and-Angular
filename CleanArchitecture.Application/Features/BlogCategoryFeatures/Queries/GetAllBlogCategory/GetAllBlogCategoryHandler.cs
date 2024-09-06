using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;

public sealed class GetAllBlogCategoryQueryHandler : IRequestHandler<GetAllBlogCategoryQuery, PaginationResult<BlogCategory>>
{
    private readonly IBlogCategoryService _blogCategoryService;

    public GetAllBlogCategoryQueryHandler(IBlogCategoryService blogCategoryService)
    {
        _blogCategoryService = blogCategoryService;
    }

    public async Task<PaginationResult<BlogCategory>> Handle(GetAllBlogCategoryQuery request, CancellationToken cancellationToken)
    {
        PaginationResult<BlogCategory> blogCategories = await _blogCategoryService.GetAllAsync(request, cancellationToken);

        return blogCategories;
    }
}
