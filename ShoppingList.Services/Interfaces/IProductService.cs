using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;

namespace ShoppingList.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> CreateProductAsync(CreateProductInputModel model);

        Task<ProductViewModel> EditProductAsync(EditProductInputModel model);

        Task<ProductViewModel> GetProductViewModelByIdAsync(int id);

        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
    }
}
