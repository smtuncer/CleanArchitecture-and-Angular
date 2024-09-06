using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;

public sealed class GetAllBlogCategoryQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllBlogCategoryQuery, PaginationResult<BlogCategory>>
{

    public async Task<PaginationResult<BlogCategory>> Handle(GetAllBlogCategoryQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var query = unitOfWork.Repository<BlogCategory>()
                                   .GetWhere(x => x.CategoryName.ToLower().Contains(request.Search.ToLower()))
                                   .OrderByDescending(x => x.UpdatedDate);

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedData = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                       .Take(request.PageSize)
                                       .AsNoTracking()
                                       .ToListAsync(cancellationToken);

            var paginationResult = mapper.Map<PaginationResult<BlogCategory>>(pagedData);

            paginationResult.PageNumber = request.PageNumber;
            paginationResult.PageSize = request.PageSize;
            paginationResult.TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
            paginationResult.IsFirstPage = paginationResult.PageNumber == 1;
            paginationResult.IsLastPage = paginationResult.PageNumber == paginationResult.TotalPages;
            return paginationResult;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }

    }
}
