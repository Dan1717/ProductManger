using System.Collections.Generic;

namespace ProductsManager.Models.DTO.Product
{
    public class ProductGetAll
    {
        public IEnumerable<ProductGet> Products { get; set; }
    }
}
