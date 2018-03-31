using System.Linq;

namespace ProductsManager.DataAccess.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int Id);
        IQueryable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
