using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;
using ShoppingList.Models.ProductsCategories;

namespace ShoppingList.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> CreateProductAsync(CreateProductInputModel model);

        Task<ProductViewModel> EditProductAsync(EditProductInputModel model);

        Task<ProductWithCategoriesViewModel> GetProductWithCategoriesViewModelByIdAsync(int id);

        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
    }
}
