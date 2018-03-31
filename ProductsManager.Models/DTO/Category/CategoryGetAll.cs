using System.Collections.Generic;

namespace ProductsManager.Models.DTO.Category
{
    public class CategoryGetAll
    {
        public IEnumerable<CategoryGet> Categories { get; set; }
    }
}
