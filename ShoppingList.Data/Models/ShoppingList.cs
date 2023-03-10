using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Data.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<ShoppingListsProducts> ShoppingListsProducts { get; set; }
    }
}
