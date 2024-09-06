using Microsoft.EntityFrameworkCore.Storage;

namespace CleanArchitecture.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    // Herhangi bir entity için generic repository'yi döndürür
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    // Veritabanı değişikliklerini asenkron olarak kaydeder
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    // Veritabanı değişikliklerini senkron olarak kaydeder
    int SaveChanges();

    // Transaction işlemlerini asenkron başlatır
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    // Transaction'ı asenkron commit eder
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    // Transaction'ı asenkron rollback yapar
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
