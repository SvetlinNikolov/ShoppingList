using ShoppingList.Models.Categories;
using ShoppingList.Models.ShoppingLists;

namespace ShoppingList.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingListViewModel> CreateShoppingListAsync(CreateShoppingListInputModel model);

        Task<ShoppingListCollectionViewModel> GetAllShoppingListsAsync();

        Task<ShoppingListViewModel> GetShoppingListViewModelByIdAsync(int id);

        Task<ShoppingListViewModel> EditShoppingListAsync(EditShoppingListInputModel model);
    }
}
