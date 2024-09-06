using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Data.Context;

namespace CleanArchitecture.Persistance.Repositories;

public sealed class UserRoleRepository : GenericRepository<UserRole, AppDbContext>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context)
    {
    }
}
