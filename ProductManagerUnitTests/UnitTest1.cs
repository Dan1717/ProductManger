using AutoMapper;
using ProductsManager.DataAccess.UnitWork;
using ProductsManager.Models.DTO.Product;
using ProductsManager.Services.ProductServices;
using System;
using Xunit;

namespace ProductManagerUnitTests
{
    public class UnitTest1
    {
        private readonly IProductService _productService;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitTest1(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productService = new ProductService(unitOfWork, mapper);
        }

        [Fact]
        public void Test1()
        {
            var result = _productService.GetAll();
            Assert.Single(result.Response.Products);
        }
    }
}
