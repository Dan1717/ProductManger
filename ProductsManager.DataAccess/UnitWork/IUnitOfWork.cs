using ProductsManager.DataAccess.Repository;
using ProductsManager.Models.DAO;

namespace ProductsManager.DataAccess.UnitWork
{
    public interface IUnitOfWork
    {
        IBaseRepository<Category> CategoryRepository { get; }
        IBaseRepository<Product> ProductRepository { get; }
        int Save();
    }
}
