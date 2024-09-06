using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services;
using MediatR;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
public sealed class CreateBlogCategoryCommandHandler : IRequestHandler<CreateBlogCategoryCommand, MessageResponse>
{
    private readonly IBlogCategoryService _blogCategoryService;

    public CreateBlogCategoryCommandHandler(IBlogCategoryService blogCategoryService)
    {
        _blogCategoryService = blogCategoryService;
    }

    public async Task<MessageResponse> Handle(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
    {
        await _blogCategoryService.CreateAsync(request, cancellationToken);
        return new("Blog Kategorisi başarıyla kaydedildi!");
    }
}
