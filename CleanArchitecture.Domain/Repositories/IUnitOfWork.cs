using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArchitecture.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    // Herhangi bir entity için generic repository'yi döndürür
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    int SaveChanges();

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
