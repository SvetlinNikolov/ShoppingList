using ShoppingList.Models.Products;

namespace ShoppingList.Models.ShoppingLists
{
    public class EditShoppingListInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> ProductIds { get; set; }
    }
}
