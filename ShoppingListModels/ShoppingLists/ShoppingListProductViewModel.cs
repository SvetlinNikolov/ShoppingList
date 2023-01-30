namespace ShoppingList.Models.ShoppingLists
{
    public class ShoppingListProductViewModel
    {
        public int ShoppingListId { get; set; }

        public int ProductId { get; set; }

        public bool ProductIsBought { get; set; }

        public string ProductName { get; set; }
    }
}
