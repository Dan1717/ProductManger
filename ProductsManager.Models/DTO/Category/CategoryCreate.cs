namespace ProductsManager.Models.DTO.Category
{
    public class CategoryCreate
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
