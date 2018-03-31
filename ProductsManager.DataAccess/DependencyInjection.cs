using Microsoft.Extensions.DependencyInjection;
using ProductsManager.DataAccess.Context;
using ProductsManager.DataAccess.Repository;
using ProductsManager.DataAccess.UnitWork;

namespace ProductsManager.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection UseDataAccessLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DataContext>();
            serviceCollection.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            return serviceCollection;
        }
    }
}
