using AutoMapper;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.UpdateBlogCategory;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.DeleteBlogCategoryById;
internal sealed class DeleteBlogCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<DeleteBlogCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteBlogCategoryCommand request, CancellationToken cancellationToken)
    {
        BlogCategory? blogCategory = await unitOfWork.Repository<BlogCategory>()
            .GetByExpressionAsync(p => p.Id == request.Id.ToString(), cancellationToken);

        if (blogCategory is null)
        {
            return Result<string>.Failure("Kategori bulunamadı");
        }

        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {

            await unitOfWork.Repository<BlogCategory>().DeleteByIdAsync(request.Id.ToString());
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result<string>.Succeed("Kategori Silindi");
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<string>.Failure("Kategori silinirken hata oluştu");
            throw;
             
        }
    }
}
