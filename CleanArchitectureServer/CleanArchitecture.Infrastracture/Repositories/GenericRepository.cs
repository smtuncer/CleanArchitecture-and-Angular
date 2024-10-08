﻿using CleanArchitecture.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastracture.Repositories;
public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class where TContext : DbContext
{
    private readonly TContext _context;

    private DbSet<TEntity> _entity;

    public GenericRepository(TContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _entity.AddAsync(entity, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
    }

    public async Task AddRangeASync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _entity.AddRangeAsync(entities, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
    }

    public void Delete(TEntity entity)
    {
        _entity.Remove(entity);
    }

    public async Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await _entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);
        _entity.Remove(entity);
    }

    public async Task DeleteByIdAsync(string id)
    {
        TEntity entity = await _entity.FindAsync(id).ConfigureAwait(continueOnCapturedContext: false);
        _entity.Remove(entity);
    }

    public void DeleteRange(ICollection<TEntity> entities)
    {
        _entity.RemoveRange(entities);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _entity.AsNoTracking().AsQueryable();
    }

    public TEntity GetByExpression(Expression<Func<TEntity, bool>> expression)
    {
        return _entity.Where(expression).AsNoTracking().FirstOrDefault();
    }

    public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);
    }

    public TEntity GetFirst()
    {
        return _entity.AsNoTracking().FirstOrDefault();
    }

    public async Task<TEntity> GetFirstAsync(CancellationToken cancellationToken = default)
    {
        return await _entity.AsNoTracking().FirstOrDefaultAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
    }

    // GetPagedAsync sayfalama sonucunu bir liste olarak döndürüyor
    public async Task<List<TEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _entity.AsQueryable();

        // Sayfalama işlemi
        var pagedData = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .AsNoTracking()
                                   .ToListAsync(cancellationToken);

        return pagedData;
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
    {
        return _entity.AsNoTracking().Where(expression).AsQueryable();
    }

    public void Update(TEntity entity)
    {
        _entity.Update(entity);
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        _entity.UpdateRange(entities);
    }
}
