using AutoMapper;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        BlogCategory blogCategory = _mapper.Map<BlogCategory>(request);
        await _unitOfWork.Repository<BlogCategory>().AddAsync(blogCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<BlogCategory>> GetAllAsync(GetAllBlogCategoryQuery request, CancellationToken cancellationToken)
    {
        IList<BlogCategory> blogCategories = await _unitOfWork.Repository<BlogCategory>().GetAll().ToListAsync(cancellationToken);

        return blogCategories;
    }
}
