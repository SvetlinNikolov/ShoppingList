using ShoppingList.Models.Categories;

namespace ShoppingList.Models.Products
{
    public class CreateProductInputModel
    {
        public string Name { get; set; }

        public CategoryCollectionViewModel CategoryCollection { get; set; }

        public int CategoryId { get; set; }
    }
}
