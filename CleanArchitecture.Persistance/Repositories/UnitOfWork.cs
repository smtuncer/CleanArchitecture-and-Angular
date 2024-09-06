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

    // Generic repository'yi döndüren metod
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        // Entity tipi
        var entityType = typeof(TEntity);

        // Eğer repository yoksa yeni bir repository oluşturup cache'e ekliyoruz
        if (!_repositories.ContainsKey(entityType))
        {
            // Yeni bir GenericRepository<TEntity> oluşturuyoruz
            var repositoryInstance = new GenericRepository<TEntity, AppDbContext>(_context);
            _repositories.TryAdd(entityType, repositoryInstance);
        }

        // Cache'teki repository'yi döndürüyoruz
        return (IGenericRepository<TEntity>)_repositories[entityType];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
