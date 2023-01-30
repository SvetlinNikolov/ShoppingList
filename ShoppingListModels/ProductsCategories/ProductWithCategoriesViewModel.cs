using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;
using ShoppingList.Models.ShoppingLists;

namespace ShoppingList.Models.ProductsCategories
{
    public class ProductWithCategoriesAndShoppingListsViewModel
    {
        public ProductViewModel Product { get; set; }

        public CategoryCollectionViewModel CategoryCollection { get; set; }

        public ShoppingListCollectionViewModel ShoppingListCollection { get; set; }
    }
}
