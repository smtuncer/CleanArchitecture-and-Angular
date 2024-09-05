using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Data.Context;

namespace CleanArchitecture.Persistance.Repositories;
public class BlogCategoryRepository : GenericRepository<BlogCategory, AppDbContext>, IBlogCategoryRepository
{
    public BlogCategoryRepository(AppDbContext context) : base(context)
    {
    }

}