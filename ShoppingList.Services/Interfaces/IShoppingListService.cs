namespace ShoppingList.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingListViewModel> CreateShoppingListAsync(CreateShoppingListInputModel model);
    }
}
