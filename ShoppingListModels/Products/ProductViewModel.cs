using ShoppingList.Models.Categories;

namespace ShoppingList.Models.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryViewModel Category { get; set; }
    }
}
