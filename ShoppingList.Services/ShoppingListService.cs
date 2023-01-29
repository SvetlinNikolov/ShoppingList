using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models.Categories;
using ShoppingList.Models.ShoppingLists;
using ShoppingList.Repositories.Interfaces;
using ShoppingList.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<ShoppingListCollectionViewModel> GetAllShoppingListsAsync()
        {
            var shoppingLists = await this.dbContext.ShoppingLists
                .OrderBy(x => x.Id)
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

            var updatedShoppingList = await this.entityRepository.EditEntityAsync(model, shoppingList);

            var viewModel = this.mapper.Map<ShoppingListViewModel>(updatedShoppingList);
            return viewModel;
        }

        private async Task<ShoppingList.Data.Models.ShoppingList> GetShoppingListByIdAsync(int id)
        {
            return await this.dbContext.ShoppingLists
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
