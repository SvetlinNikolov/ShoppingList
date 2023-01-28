using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Data.Models
{
    public class ProductsBought
    {
        public virtual ShoppingList ShoppingList { get; set; }

        public int ShoppingListId { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        public bool ProductIsBought { get; set; }
    }
}
