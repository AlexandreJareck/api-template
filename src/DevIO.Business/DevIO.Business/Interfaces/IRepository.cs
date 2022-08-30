using DevIO.Business.Models;
using System.Linq.Expressions;

namespace DevIO.Business.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task Add(TEntity entity);
    Task<TEntity> GetById(Guid id);
    Task<List<TEntity>> GetAll();
    Task Update(TEntity obj);
    Task Remove(Guid id);
    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

    Task<int> SaveChange();
}
