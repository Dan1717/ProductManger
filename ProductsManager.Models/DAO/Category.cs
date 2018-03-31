using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace ProductsManager.Models.DAO
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public static void Build(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Category> entityBuilder = modelBuilder.Entity<Category>();
            entityBuilder.HasKey(m => m.Id);
        }
    }
}
