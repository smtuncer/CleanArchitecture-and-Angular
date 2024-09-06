using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace CleanArchitecture.Persistance.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;
    private readonly ConcurrentDictionary<Type, object> _repositories = new ConcurrentDictionary<Type, object>();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    // Repository erişimi
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var entityType = typeof(TEntity);

        // Eğer repository cache'te yoksa yeni bir repository oluştur ve cache'e ekle
        if (!_repositories.ContainsKey(entityType))
        {
            var repositoryInstance = new GenericRepository<TEntity, AppDbContext>(_context);
            _repositories.TryAdd(entityType, repositoryInstance);
        }

        // Cache'teki repository'yi döndür
        return (IGenericRepository<TEntity>)_repositories[entityType];
    }

    // Asenkron SaveChanges
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    // Senkron SaveChanges
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    // Transaction'ı asenkron başlat
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction;
    }

    // Transaction'ı asenkron commit et
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }
    }

    // Transaction'ı asenkron rollback yap
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }
    }

    // Kaynakları serbest bırak
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
