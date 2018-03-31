using ProductsManager.Models.DTO.Category;

namespace ProductsManager.Services.CategoryService
{
    public interface ICategoryService
    {
        ServiceResponse<int> Create(CategoryCreate request);
        ServiceResponse Delete(int id);
        ServiceResponse Update(CategoryUpdate request);
	    ServiceResponse<CategoryGet> Get(int id);
        ServiceResponse<CategoryGetAll> GetAll();
    }
}
