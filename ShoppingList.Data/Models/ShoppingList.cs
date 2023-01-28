using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Data.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser User { get; set; }

        //public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductsBought> ProductsBought { get; set; }
    }
}
