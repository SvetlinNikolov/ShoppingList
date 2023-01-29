using ShoppingList.Models.Categories;

namespace ShoppingList.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateCategoryAsync(CreateCategoryInputModel model);

        Task<CategoryViewModel> EditCategoryAsync(EditCategoryInputModel model);

        Task<CategoryViewModel> GetCategoryViewModelByIdAsync(int id);

        Task<CategoryCollectionViewModel> GetAllCategoriesAsync();
    }
}
