using ShoppingList.Models.Products;

namespace ShoppingList.Models.ShoppingLists
{
    public class ShoppingListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ShoppingListProductViewModel> Products { get; set; }
    }
}
