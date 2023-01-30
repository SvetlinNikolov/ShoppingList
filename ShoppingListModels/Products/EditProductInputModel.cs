namespace ShoppingList.Models.Products
{
    public class EditProductInputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> ShoppingListIds { get; set; }
    }
}
