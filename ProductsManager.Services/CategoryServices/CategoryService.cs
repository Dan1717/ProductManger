using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductsManager.DataAccess.UnitWork;
using ProductsManager.Models.DTO.Category;
using System;

namespace ProductsManager.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public ServiceResponse<int> Create(CategoryCreate request)
        {
            var serviceResponse = new ServiceResponse<int>();
            try
            {
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

                if (request.ParentCategoryId != null)
                {
                    var parentCategory = _unitOfWork.CategoryRepository.GetById(request.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        serviceResponse.Errors.Add("Request", $"Does not exists parentCategoryId = {request.ParentCategoryId}");
                        serviceResponse.StatusCode = 404;
                    } else
                    {
                        _logger.LogDebug("Successfully got data from db");
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
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                serviceResponse.Errors.Add("Request", $"AAAAAAAAAAAA, WHAT HAPPENED??!?!?!?!??");
                serviceResponse.StatusCode = 500;
            }

            return serviceResponse;
        }

        public ServiceResponse Delete(int id)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
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

                    _logger.LogDebug("Successfully got data from db");
                }
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                serviceResponse.Errors.Add("Request", $"AAAAAAAAAAAA, WHAT HAPPENED??!?!?!?!??");
                serviceResponse.StatusCode = 500;
            }

            return serviceResponse;
        }

        public ServiceResponse Update(CategoryUpdate request)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
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

                    _logger.LogDebug("Successfully got data from db");
                    serviceResponse.StatusCode = 204;
                }
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                serviceResponse.Errors.Add("Request", $"AAAAAAAAAAAA, WHAT HAPPENED??!?!?!?!??");
                serviceResponse.StatusCode = 500;
            }
            return serviceResponse;
        }

        public ServiceResponse<CategoryGet> Get(int id)
        {
            var serviceResponse = new ServiceResponse<CategoryGet>();
            try
            {
                var category = _unitOfWork.CategoryRepository.GetById(id);

                if (category == null)
                {
                    serviceResponse.Errors.Add(nameof(id), $"Category with id {id} does not exists!");
                    serviceResponse.StatusCode = 404;
                }

                if (serviceResponse.IsSuccess)
                {
                    serviceResponse.Response = _mapper.Map<CategoryGet>(category);
                    _logger.LogDebug("Successfully got data from db");

                }
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                serviceResponse.Errors.Add("Request", $"AAAAAAAAAAAA, WHAT HAPPENED??!?!?!?!??");
                serviceResponse.StatusCode = 500;
            }
            return serviceResponse;
        }

        public ServiceResponse<CategoryGetAll> GetAll()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            _logger.LogDebug("Successfully got data from db");
            _logger.LogError("Test log, Nothing dropped");
            var serviceResponse = new ServiceResponse<CategoryGetAll>
            {
                Response = _mapper.Map<CategoryGetAll>(categories)
            };

            return serviceResponse;
        }
    }
}
