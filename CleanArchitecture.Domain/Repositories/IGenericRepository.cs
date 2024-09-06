using System.Linq.Expressions;

namespace CleanArchitecture.Domain.Repositories;
public interface IGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);

    Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken));
    Task<TEntity> GetFirstAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    Task AddRangeASync(ICollection<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

    void Update(TEntity entity);
    void UpdateRange(ICollection<TEntity> entities);

    Task DeleteByIdAsync(string id);
    Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken));
    void Delete(TEntity entity);
    void DeleteRange(ICollection<TEntity> entities);
    Task<List<TEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default(CancellationToken));
}
