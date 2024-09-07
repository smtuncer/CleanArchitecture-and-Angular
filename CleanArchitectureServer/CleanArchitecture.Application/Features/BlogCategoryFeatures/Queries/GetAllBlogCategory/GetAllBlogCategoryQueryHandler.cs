using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TS.Result;

namespace CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;

public sealed class GetAllBlogCategoryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllBlogCategoryQuery, Result<List<BlogCategory>>>
{

    public async Task<Result<List<BlogCategory>>> Handle(GetAllBlogCategoryQuery request, CancellationToken cancellationToken)
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
            return pagedData;
        }
        catch (Exception)
        {
            throw;
        }

    }
}
