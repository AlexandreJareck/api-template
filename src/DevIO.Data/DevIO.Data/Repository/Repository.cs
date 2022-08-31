using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevIO.Data.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly MyDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(MyDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
        return await DbSet.FindAsync(id) ?? new TEntity();
    }
    public virtual async Task<List<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }
    public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task Add(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChange();
    }
    public virtual async Task Update(TEntity obj)
    {
        DbSet.Update(obj);
        await SaveChange();
    }

    public virtual async Task Remove(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChange();
    }

    public async Task<int> SaveChange()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}
