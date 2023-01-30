namespace ShoppingList.Models.Products
{
    public class AddProductToShoppingListsInputModel
    {
        public int ProductId { get; set; }

        public IEnumerable<int> ShoppingListIds { get; set; }
    }
}
