using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;
using ShoppingList.Models.ShoppingLists;

namespace ShoppingList.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingListViewModel> CreateShoppingListAsync(CreateShoppingListInputModel model);

        Task<ShoppingListCollectionViewModel> GetAllShoppingLists();

        Task<ShoppingListViewModel> GetShoppingListViewModelByIdAsync(int id);

        Task<ShoppingListViewModel> EditShoppingListAsync(EditShoppingListInputModel model);

        Task<bool> AddProductToShoppingListsAsync(AddProductToShoppingListsInputModel model);
    }
}
