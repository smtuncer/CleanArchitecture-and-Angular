using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Services;
public sealed class BlogCategoryService : IBlogCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlogCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task CreateAsync(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            BlogCategory blogCategory = _mapper.Map<BlogCategory>(request);

            await _unitOfWork.Repository<BlogCategory>().AddAsync(blogCategory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            // Bir hata oluşursa transaction'ı rollback yapıyoruz
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;  // Hata tekrar fırlatılır, üst katmanda yakalanabilir
        }
    }


    public async Task<PaginationResult<BlogCategory>> GetAllAsync(GetAllBlogCategoryQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<BlogCategory>()
                               .GetWhere(x => x.CategoryName.ToLower().Contains(request.Search.ToLower()))
                               .OrderByDescending(x => x.UpdatedDate);

        var totalCount = await query.CountAsync(cancellationToken);

        var pagedData = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .AsNoTracking()
                                   .ToListAsync(cancellationToken);

        var paginationResult = _mapper.Map<PaginationResult<BlogCategory>>(pagedData);

        paginationResult.PageNumber = request.PageNumber;
        paginationResult.PageSize = request.PageSize;
        paginationResult.TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
        paginationResult.IsFirstPage = paginationResult.PageNumber == 1;
        paginationResult.IsLastPage = paginationResult.PageNumber == paginationResult.TotalPages;

        return paginationResult;
    }

}