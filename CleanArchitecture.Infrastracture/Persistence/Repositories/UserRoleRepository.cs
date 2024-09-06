using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Persistance.Data.Context;

namespace CleanArchitecture.Infrastructure.Persistance.Repositories;

public sealed class UserRoleRepository : GenericRepository<UserRole, AppDbContext>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context)
    {
    }
}
