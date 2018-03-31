using ProductsManager.DataAccess.Context;
using ProductsManager.DataAccess.Repository;
using ProductsManager.Models.DAO;
using System;

namespace ProductsManager.DataAccess.UnitWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dbContext;

        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IBaseRepository<Product> _productRepository;

        public UnitOfWork(DataContext dbContext, IBaseRepository<Category> categoryRepository, IBaseRepository<Product> productRepository) {
	        _dbContext = dbContext;

            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public IBaseRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository;
            }
        }
        public IBaseRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository;
            }
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose
    }
}
