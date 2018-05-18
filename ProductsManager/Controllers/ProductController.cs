using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsManager.Models.DTO.Product;
using ProductsManager.Services.ProductServices;
using ProductsManager.WebApi.ServiceResponce;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        [SwaggerResponse(200, typeof(ProductGet))]
        public IActionResult Get(int id)
        {
            var productResponse = _productService.Get(id);
            return productResponse.ToJsonResult();
        }

        // GET: api/Product
        [HttpGet]
        [SwaggerResponse(200, typeof(ProductGetAll))]
        public IActionResult Get()
        {
            var productResponse = _productService.GetAll();
            return productResponse.ToJsonResult();
        }

        // POST: api/Product
        [HttpPost]
        [SwaggerResponse(201)]
        public IActionResult Post([FromBody]ProductsCreate product)
        {
            var productResponse = _productService.Create(product);
            return productResponse.ToJsonResult();
        }
        
        // PUT: api/Product/5
        [HttpPut("{id}")]
        [SwaggerResponse(200)]
        public IActionResult Put(int id, [FromBody]ProductUpdate product)
        {
            product.Id = id;
            var productResponse = _productService.Update(product);
            return productResponse.ToJsonResult();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [SwaggerResponse(200)]
        public IActionResult Delete(int id)
        {
            return _productService.Delete(id).ToJsonResult();
        }
    }
}
