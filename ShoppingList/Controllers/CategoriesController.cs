using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models.Categories;
using ShoppingList.Services.Interfaces;
using ShoppingList.Views.Shared;

namespace ShoppingList.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> AllCategories()
        {
            var categories = await this.categoryService.GetAllCategoriesAsync();

            return this.View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetails(int id)
        {
            var category = await this.categoryService.GetCategoryViewModelByIdAsync(id);

            if (category == null)
            {
                return this.View("Error", new ErrorModel("Category does not exist"));
            }

            return this.View(category);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryInputModel model)
        {
            var category = await this.categoryService.CreateCategoryAsync(model);

            if (category == null)
            {
                return this.View("Error", new ErrorModel("Error when creating category"));
            }

            return this.RedirectToAction(nameof(AllCategories));
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(EditCategoryInputModel model)
        {
            var editedCategory = await this.categoryService.EditCategoryAsync(model);

            if (editedCategory == null)
            {
                return this.View("Error", new ErrorModel("Error when editing category"));
            }

            return this.RedirectToAction(nameof(AllCategories));
        }
    }
}
