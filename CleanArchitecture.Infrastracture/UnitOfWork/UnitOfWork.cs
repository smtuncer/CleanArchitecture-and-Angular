using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastracture.Data.Context;
using CleanArchitecture.Infrastracture.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace CleanArchitecture.Infrastracture.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;
    private readonly ConcurrentDictionary<Type, object> _repositories = new ConcurrentDictionary<Type, object>();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var entityType = typeof(TEntity);

        if (!_repositories.ContainsKey(entityType))
        {
            var repositoryInstance = new GenericRepository<TEntity, AppDbContext>(_context);
            _repositories.TryAdd(entityType, repositoryInstance);
        }

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
