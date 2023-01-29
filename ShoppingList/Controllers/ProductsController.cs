using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;
using ShoppingList.Services.Interfaces;
using ShoppingList.Views.Shared;

namespace ShoppingList.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> AllProducts()
        {
            var products = await this.productService.GetAllProductsAsync();

            return this.View(products);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await this.categoryService.GetAllCategoriesAsync();

            var viewModel = new CreateProductInputModel
            {
                CategoryCollection = new CategoryCollectionViewModel
                {
                    Categories = categories.Categories
                }
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductInputModel model)
        {
            var product = await this.productService.CreateProductAsync(model);

            if (product == null)
            {
                return this.View("Error", new ErrorModel("Error when creating product"));
            }

            return this.RedirectToAction(nameof(AllProducts));
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await this.productService.GetProductWithCategoriesViewModelByIdAsync(id);

            if (product == null)
            {
                return this.View("Error", new ErrorModel("Product does not exist"));
            }

            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductInputModel model)
        {
            var editedProduct = await this.productService.EditProductAsync(model);

            if (editedProduct == null)
            {
                return this.View("Error", new ErrorModel("Error when editing product"));
            }

            return this.RedirectToAction(nameof(AllProducts));
        }
    }
}
