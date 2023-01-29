using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;

namespace ShoppingList.Models.ProductsCategories
{
    public class ProductWithCategoriesViewModel
    {
        public ProductViewModel Product { get; set; }

        public CategoryCollectionViewModel CategoryCollection { get; set; }
    }
}
