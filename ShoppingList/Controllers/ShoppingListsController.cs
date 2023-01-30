using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Data.Models;
using ShoppingList.Models.Categories;
using ShoppingList.Models.ShoppingLists;
using ShoppingList.Services.Interfaces;
using ShoppingList.Views.Shared;

namespace ShoppingList.Controllers
{
    [Authorize]
    public class ShoppingListsController : Controller
    {
        private readonly IShoppingListService shoppingListService;

        public ShoppingListsController(
          IShoppingListService shoppingListService)
        {
            this.shoppingListService = shoppingListService;
        }

        [HttpGet]
        public async Task<IActionResult> AllShoppingLists()
        {
            var shoppingLists = await this.shoppingListService.GetAllShoppingLists();

            return this.View(shoppingLists);
        }

        [HttpGet]
        public IActionResult CreateShoppingList()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingList(CreateShoppingListInputModel model)
        {
            var shoppingList = await this.shoppingListService.CreateShoppingListAsync(model);

            if (shoppingList == null)
            {
                return this.View("Error", new ErrorModel("Error when creating Shopping List"));
            }

            return this.RedirectToAction(nameof(AllShoppingLists));
        }

        [HttpGet]
        public async Task<IActionResult> ShoppingListDetails(int id)
        {
            var shoppingList = await this.shoppingListService.GetShoppingListViewModelByIdAsync(id);

            if (shoppingList == null)
            {
                return this.View("Error", new ErrorModel("Shopping List does not exist"));
            }

            return this.View(shoppingList);
        }

        [HttpPost]
        public async Task<IActionResult> EditShoppingList(EditShoppingListInputModel model)
        {
            var editedShoppingList = await this.shoppingListService.EditShoppingListAsync(model);

            if (editedShoppingList == null)
            {
                return this.View("Error", new ErrorModel("Error when editing Shopping List"));
            }

            return this.RedirectToAction(nameof(AllShoppingLists));
        }
    }
}
