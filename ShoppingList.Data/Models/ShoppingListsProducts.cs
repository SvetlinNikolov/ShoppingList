using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Data.Models
{
    public class ShoppingListsProducts
    {
        public int Id { get; set; }

        public virtual ShoppingList ShoppingList { get; set; }

        public int ShoppingListId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        public bool ProductIsBought { get; set; }
    }
}
