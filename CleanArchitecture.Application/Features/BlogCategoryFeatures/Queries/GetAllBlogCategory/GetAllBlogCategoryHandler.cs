using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;

public sealed class GetAllCarQueryHandler : IRequestHandler<GetAllBlogCategoryQuery, IList<BlogCategory>>
{
    private readonly IBlogCategoryService _blogCategoryService;

    public GetAllCarQueryHandler(IBlogCategoryService blogCategoryService)
    {
        _blogCategoryService = blogCategoryService;
    }

    public async Task<IList<BlogCategory>> Handle(GetAllBlogCategoryQuery request, CancellationToken cancellationToken)
    {
        IList<BlogCategory> blogCategories = await _blogCategoryService.GetAllAsync(request, cancellationToken);
        return blogCategories;
    }
}
