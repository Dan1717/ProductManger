using AutoMapper;
using ProductsManager.DataAccess.UnitWork;
using ProductsManager.Models.DAO;
using ProductsManager.Models.DTO.Product;

namespace ProductsManager.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ServiceResponse<int> Create(ProductsCreate request)
        {
            var serviceResponse = new ServiceResponse<int>();
            if (request == null)
            {
                serviceResponse.Errors.Add("Request", "Should not be null");
                serviceResponse.StatusCode = 400;
            }
            if (request.Name == null)
            {
                serviceResponse.Errors.Add("Request", "Name should not be null");
                serviceResponse.StatusCode = 400;
            }

                var category = _unitOfWork.CategoryRepository.GetById(request.CategoryId);
                if (category == null)
                {
                    serviceResponse.Errors.Add("Request", $"Does not exists categoryId = {request.CategoryId}");
                    serviceResponse.StatusCode = 404;
                }

            if (serviceResponse.IsSuccess)
            {
                var product = _mapper.Map<Product>(request);
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.Save();

                serviceResponse.Response = product.Id;
                serviceResponse.StatusCode = 201;
            }

            return serviceResponse;
        }

        public ServiceResponse Delete(int id)
        {
            var serviceResponse = new ServiceResponse();
            var product = _unitOfWork.ProductRepository.GetById(id);

            if (product == null)
            {
                serviceResponse.Errors.Add(nameof(id), $"Product with id {id} does not exists!");
                serviceResponse.StatusCode = 404;
            }

            if (serviceResponse.IsSuccess)
            {
                _unitOfWork.ProductRepository.Delete(product);
                _unitOfWork.Save();
            }

            return serviceResponse;
        }

        public ServiceResponse Update(ProductUpdate request)
        {
            var serviceResponse = new ServiceResponse();
            if (request == null)
            {
                serviceResponse.Errors.Add("Request", "Should not be null");
            }

            var product = _unitOfWork.ProductRepository.GetById(request.Id);

            if (product == null)
            {
                serviceResponse.Errors.Add(nameof(request.Id), $"Product with Id = {request.Id} doesnt exsits");
                serviceResponse.StatusCode = 400;
            }

            var category = _unitOfWork.CategoryRepository.GetById(request.CategoryId);

            if (category == null)
            {
                serviceResponse.Errors.Add(nameof(request.Id), $"Category with Id = {request.Id} doesnt exsits");
                serviceResponse.StatusCode = 400;
            }

            if (serviceResponse.IsSuccess)
            {
                product = _mapper.Map<Product>(request);
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Save();

                serviceResponse.StatusCode = 204;
            }
            return serviceResponse;
        }

        public ServiceResponse<ProductGet> Get(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);

            var serviceResponse = new ServiceResponse<ProductGet>();
            if (product == null)
            {
                serviceResponse.Errors.Add(nameof(id), $"Product with id {id} does not exists!");
                serviceResponse.StatusCode = 404;
            }

            if (serviceResponse.IsSuccess)
            {
                serviceResponse.Response = _mapper.Map<ProductGet>(product);
            }

            return serviceResponse;
        }

        public ServiceResponse<ProductGetAll> GetAll()
        {
            var products = _unitOfWork.ProductRepository.GetAll();
            var serviceResponse = new ServiceResponse<ProductGetAll>
            {
                Response = _mapper.Map<ProductGetAll>(products)
            };

            return serviceResponse;
        }
    }
}
