using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Persistance.Data.Context;

namespace CleanArchitecture.Infrastructure.Persistance.Repositories;
public class BlogCategoryRepository : GenericRepository<BlogCategory, AppDbContext>, IBlogCategoryRepository
{
    public BlogCategoryRepository(AppDbContext context) : base(context)
    {
    }

}