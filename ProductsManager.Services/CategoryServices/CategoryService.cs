using AutoMapper;
using ProductsManager.DataAccess.UnitWork;
using ProductsManager.Models.DTO.Category;

namespace ProductsManager.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ServiceResponse<int> Create(CategoryCreate request)
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

            if (request.ParentCategoryId != null) { 
                var parentCategory = _unitOfWork.CategoryRepository.GetById(request.ParentCategoryId.Value);
                if (parentCategory == null)
                {
                    serviceResponse.Errors.Add("Request", $"Does not exists parentCategoryId = {request.ParentCategoryId}");
                    serviceResponse.StatusCode = 404;
                }
            }

            if (serviceResponse.IsSuccess)
            {
                var category = _mapper.Map<Models.DAO.Category>(request);
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();

                serviceResponse.Response = category.Id;
                serviceResponse.StatusCode = 201;
            }

            return serviceResponse;
        }

        public ServiceResponse Delete(int id)
        {
            var serviceResponse = new ServiceResponse();
            var category = _unitOfWork.CategoryRepository.GetById(id);

            if (category == null)
            {
                serviceResponse.Errors.Add(nameof(id), $"Category with id {id} does not exists!");
                serviceResponse.StatusCode = 404;
            }

            if (serviceResponse.IsSuccess)
            {
                _unitOfWork.CategoryRepository.Delete(category);
                _unitOfWork.Save();
            }

            return serviceResponse;
        }

        public ServiceResponse Update(CategoryUpdate request)
        {
            var serviceResponse = new ServiceResponse();
            if (request == null)
            {
                serviceResponse.Errors.Add("Request", "Should not be null");
            }

            var category = _unitOfWork.CategoryRepository.GetById(request.Id);

            if (category == null)
            {
                serviceResponse.Errors.Add(nameof(request.Id), $"Category with Id = {request.Id} doesnt exsits");
                serviceResponse.StatusCode = 400;
            }

            if (serviceResponse.IsSuccess)
            {
                category = _mapper.Map<Models.DAO.Category>(request);
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();

                serviceResponse.StatusCode = 204;
            }
            return serviceResponse;
        }

        public ServiceResponse<CategoryGet> Get(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);

            var serviceResponse = new ServiceResponse<CategoryGet>();
            if (category == null)
            {
                serviceResponse.Errors.Add(nameof(id), $"Category with id {id} does not exists!");
                serviceResponse.StatusCode = 404;
            }

            if (serviceResponse.IsSuccess)
            {
                serviceResponse.Response = _mapper.Map<CategoryGet>(category);
            }

            return serviceResponse;
        }

        public ServiceResponse<CategoryGetAll> GetAll()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            var serviceResponse = new ServiceResponse<CategoryGetAll>
            {
                Response = _mapper.Map<CategoryGetAll>(categories)
            };

            return serviceResponse;
        }
    }
}
