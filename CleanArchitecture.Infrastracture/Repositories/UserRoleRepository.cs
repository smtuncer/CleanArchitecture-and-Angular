using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastracture.Data.Context;

namespace CleanArchitecture.Infrastracture.Repositories;

public sealed class UserRoleRepository : GenericRepository<UserRole, AppDbContext>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context)
    {
    }
}
