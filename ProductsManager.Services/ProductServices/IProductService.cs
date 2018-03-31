using ProductsManager.Models.DTO.Product;

namespace ProductsManager.Services.ProductServices
{
    public interface IProductService
    {
        ServiceResponse<int> Create(ProductsCreate categoryCreate);
        ServiceResponse Delete(int id);
        ServiceResponse Update(ProductUpdate category);
        ServiceResponse<ProductGet> Get(int id);
        ServiceResponse<ProductGetAll> GetAll();
    }
}
