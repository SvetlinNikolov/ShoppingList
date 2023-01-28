using AutoMapper;
using ShoppingList.Data.Models;
using ShoppingList.Models.Categories;

namespace ShoppingList.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryInputModel, Category>();

            CreateMap<Category, CategoryViewModel>();

            CreateMap<EditCategoryInputModel, Category>();
        }
    }
}
