using AutoMapper;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Commands.CreateBlogCategory;
using CleanArchitecture.Application.Features.BlogCategoryFeatures.Queries.GetAllBlogCategory;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Services;
public sealed class BlogCategoryService : IBlogCategoryService
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlogCategoryService(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
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
        var data = await _unitOfWork.Repository<BlogCategory>().GetAll().ToListAsync(cancellationToken);
        IList<BlogCategory> blogCategories = _mapper.Map<IList<BlogCategory>>(data);
        return blogCategories;
    }
}
