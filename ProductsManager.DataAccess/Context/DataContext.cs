using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductsManager.Models.DAO;


namespace ProductsManager.DataAccess.Context
{
    public class DataContext : DbContext
    {
        private readonly DatabaseOptions _options;
        public DataContext(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Category.Build(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
