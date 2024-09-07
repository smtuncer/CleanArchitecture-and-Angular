using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastracture.Data.Context;

namespace CleanArchitecture.Infrastracture.Repositories;

public sealed class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
