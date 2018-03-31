using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsManager.Models.DAO
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Product> entityBuilder = modelBuilder.Entity<Product>();
            entityBuilder.HasKey(m => m.Id);
        }
    }
}
