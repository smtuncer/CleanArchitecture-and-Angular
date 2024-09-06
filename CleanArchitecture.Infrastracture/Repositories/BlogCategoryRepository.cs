using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastracture.Data.Context;

namespace CleanArchitecture.Infrastracture.Repositories;
public class BlogCategoryRepository : GenericRepository<BlogCategory, AppDbContext>, IBlogCategoryRepository
{
    public BlogCategoryRepository(AppDbContext context) : base(context)
    {
    }

}