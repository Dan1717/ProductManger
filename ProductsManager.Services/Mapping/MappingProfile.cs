using AutoMapper;
using ProductsManager.Models.DAO;
using ProductsManager.Models.DTO.Category;
using ProductsManager.Models.DTO.Product;
using System.Linq;

namespace ProductsManager.Services.Mapping
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<CategoryCreate, Category>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CategoryUpdate, Category>();

            CreateMap<IQueryable<Category>, CategoryGetAll>()
                 .ConvertUsing(MapCategoryAll);

            CreateMap<Category, CategoryGet>();

            CreateMap<ProductsCreate, Product>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Category, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProductUpdate, Product>()
                .ForMember(m => m.Category, opt => opt.Ignore());
            CreateMap<IQueryable<Product>, ProductGetAll>()
                .ConvertUsing(MapProductAll);

            CreateMap<Product, ProductGet>()
                .ForMember(m => m.Id, n => n.MapFrom(s => s.Id));

            
        }

        private ProductGetAll MapProductAll (IQueryable<Product> products)
        {
            return new ProductGetAll()
            {
                Products = products.Select(Mapper.Map<ProductGet>)
            };
        }
        private CategoryGetAll MapCategoryAll (IQueryable<Category> categories)
        {
            return new CategoryGetAll()
            {
                Categories = categories.Select(Mapper.Map<CategoryGet>)
            };
        }
    }
}
