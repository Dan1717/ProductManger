using Microsoft.AspNetCore.Mvc;
using ProductsManager.Models.DTO.Category;
using ProductsManager.Services.CategoryService;
using ProductsManager.WebApi.ServiceResponce;

namespace ProductsManager.WebApi.Controllers
{

    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/category
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
		{
	        var categoryResponse = _categoryService.Get(id);
			return categoryResponse.ToJsonResult();
		}

        // GET: api/category/5
        [HttpGet]
        public IActionResult Get() {
	        var categoryResponse = _categoryService.GetAll();
            return categoryResponse.ToJsonResult();
        }

        // POST: api/category
        [HttpPost]
        public IActionResult Post([FromBody] CategoryCreate category)
        {
            var categoryResponse = _categoryService.Create(category);
            return categoryResponse.ToJsonResult();
        }
        
        // PUT: api/category/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CategoryUpdate category)
        {
            category.Id = id;
	        var categoryResponse = _categoryService.Update(category);
			return categoryResponse.ToJsonResult();
		}
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _categoryService.Delete(id).ToJsonResult();
        }
    }
}
