using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsManager.Models.DTO.Product;
using ProductsManager.Services.ProductServices;
using ProductsManager.WebApi.ServiceResponce;

namespace ProductsManager.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var productResponse = _productService.Get(id);
            return productResponse.ToJsonResult();
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var productResponse = _productService.GetAll();
            return productResponse.ToJsonResult();
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody]ProductsCreate product)
        {
            var productResponse = _productService.Create(product);
            return productResponse.ToJsonResult();
        }
        
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductUpdate product)
        {
            product.Id = id;
            var productResponse = _productService.Update(product);
            return productResponse.ToJsonResult();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _productService.Delete(id).ToJsonResult();
        }
    }
}
