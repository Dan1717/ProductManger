using Microsoft.EntityFrameworkCore;
using ProductsManager.DataAccess.Context;
using ProductsManager.DataAccess.Repository;
using System;
using System.Linq;

namespace ProductsManager.DataAccess
{
    public class BaseRepository <TEntity> : IDisposable, IBaseRepository <TEntity> where TEntity : class
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual TEntity GetById(int entityId)
        {
            return _dbContext.Set<TEntity>().Find(entityId);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
