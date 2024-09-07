using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;
using TS.Result;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
public sealed class CreateBlogCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateBlogCategoryCommand, Result<string>>
{

    public async Task<Result<string>> Handle(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
    {
        BlogCategory blogCategory = mapper.Map<BlogCategory>(request);
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await unitOfWork.Repository<BlogCategory>().AddAsync(blogCategory);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result<string>.Succeed("Kategori eklendi");
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<string>.Failure("Kategori eklenirken hata oluştu");
            throw;
        }
    }
}
