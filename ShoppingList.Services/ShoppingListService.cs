using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models.Products;
using ShoppingList.Models.ShoppingLists;
using ShoppingList.Repositories.Interfaces;
using ShoppingList.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingList.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEntityRepository entityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingListService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IEntityRepository entityRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.entityRepository = entityRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShoppingListViewModel> CreateShoppingListAsync(CreateShoppingListInputModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Name))
            {
                var shoppingListInDb = await this.dbContext.ShoppingLists.FirstOrDefaultAsync(x => x.Name == model.Name);

                if (shoppingListInDb == null)
                {
                    model.UserId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var newShoppingList = await this.entityRepository.CreateEntityAsync<CreateShoppingListInputModel, ShoppingList.Data.Models.ShoppingList>(model);

                    var viewModel = this.mapper.Map<ShoppingListViewModel>(newShoppingList);
                    return viewModel;
                }
            }

            return default;
        }

        public async Task<ShoppingListCollectionViewModel> GetAllShoppingLists()
        {
            var userId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingLists = await this.dbContext.ShoppingLists
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var shoppingListsViewModels = this.mapper.Map<IEnumerable<ShoppingListViewModel>>(shoppingLists);

            var viewModel = new ShoppingListCollectionViewModel
            {
                ShoppingLists = shoppingListsViewModels
            };

            return viewModel;
        }

        public async Task<ShoppingListViewModel> GetShoppingListViewModelByIdAsync(int id)
        {
            var shoppingList = await this.GetShoppingListByIdAsync(id);

            if (shoppingList == null)
            {
                return default;
            }

            var viewModel = this.mapper.Map<ShoppingListViewModel>(shoppingList);
            return viewModel;
        }

        public async Task<ShoppingListViewModel> EditShoppingListAsync(EditShoppingListInputModel model)
        {
            if (model is null)
            {
                return default;
            }

            var shoppingList = await this.GetShoppingListByIdAsync(model.Id);

            if (shoppingList == null)
            {
                return default;
            }

            if (model.ProductIds?.Any() == true)
            {
                this.MarkProductAsBought(model, shoppingList);
            }

            var updatedShoppingList = await this.entityRepository.EditEntityAsync(model, shoppingList);

            var viewModel = this.mapper.Map<ShoppingListViewModel>(updatedShoppingList);
            return viewModel;
        }

        public async Task<bool> AddProductToShoppingListsAsync(AddProductToShoppingListsInputModel model)
        {
            var productToAddToShoppingLists = model.ShoppingListIds.Select(shoppingListId => new ShoppingListsProducts
            {
                ProductId = model.ProductId,
                ShoppingListId = shoppingListId,
                ProductIsBought = false
            }).ToList();

            await this.dbContext.ShoppingListsProducts.AddRangeAsync(productToAddToShoppingLists);

            return await this.dbContext.SaveChangesAsync() > 0;
        }

        private async Task<ShoppingList.Data.Models.ShoppingList> GetShoppingListByIdAsync(int id)
        {
            var userId = this.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingList = await this.dbContext.ShoppingLists
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (shoppingList == null)
            {
                return default;
            }

            var productsBought = await this.dbContext.ShoppingListsProducts
                 .Where(x => x.ShoppingListId == id && x.ShoppingList.UserId == userId)
                 .Include(x => x.Product)
                 .ToListAsync();

            shoppingList.ShoppingListsProducts = productsBought;

            return shoppingList;
        }

        private void MarkProductAsBought(EditShoppingListInputModel model, Data.Models.ShoppingList shoppingList)
        {
            var shoppingListsProducts = shoppingList.ShoppingListsProducts
                            .Where(x => model.ProductIds.Contains(x.ProductId));

            foreach (var id in model.ProductIds)
            {
                shoppingListsProducts.FirstOrDefault(x => x.ProductId == id && x.ProductIsBought == false).ProductIsBought = true;
            }
        }
    }
}
