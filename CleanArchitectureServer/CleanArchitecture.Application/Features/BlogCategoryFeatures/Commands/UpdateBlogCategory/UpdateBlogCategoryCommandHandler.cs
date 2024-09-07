using AutoMapper;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
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

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.UpdateBlogCategory;
internal class UpdateBlogCategoryCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateBlogCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateBlogCategoryCommand request, CancellationToken cancellationToken)
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
            BlogCategory blogCategoryMapped = mapper.Map<BlogCategory>(request);

            unitOfWork.Repository<BlogCategory>().Update(blogCategoryMapped);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result<string>.Succeed("Kategori Güncellendi");
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<string>.Failure("Kategori güncellenirken hata oluştu");
            throw;
        }
    }
}
