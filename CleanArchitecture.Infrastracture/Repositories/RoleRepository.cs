using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastracture.Data.Context;

namespace CleanArchitecture.Infrastracture.Repositories;

public sealed class RoleRepository : GenericRepository<UserRole, AppDbContext>, IUserRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }
}
