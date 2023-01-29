using AutoMapper;
using ShoppingList.Data.Models;
using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;

namespace ShoppingList.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryInputModel, Category>();

            CreateMap<Category, CategoryViewModel>();

            CreateMap<EditCategoryInputModel, Category>();

            CreateMap<CreateProductInputModel, Product>();

            CreateMap<Product, ProductViewModel>();

            CreateMap<EditProductInputModel, Product>();
        }
    }
}
