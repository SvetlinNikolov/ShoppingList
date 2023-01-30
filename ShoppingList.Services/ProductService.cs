using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models.Products;
using ShoppingList.Models.ProductsCategories;
using ShoppingList.Repositories.Interfaces;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEntityRepository entityRepository;
        private readonly ICategoryService categoryService;
        private readonly IShoppingListService shoppingListService;

        public ProductService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IEntityRepository entityRepository,
            ICategoryService categoryService,
            IShoppingListService shoppingListService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.entityRepository = entityRepository;
            this.categoryService = categoryService;
            this.shoppingListService = shoppingListService;
        }

        public async Task<ProductViewModel> CreateProductAsync(CreateProductInputModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Name) && model.CategoryId != default)
            {
                var productInDb = await this.dbContext.Products.FirstOrDefaultAsync(x => x.Name == model.Name);

                if (productInDb == null)
                {
                    var newProduct = await this.entityRepository.CreateEntityAsync<CreateProductInputModel, Product>(model);

                    var viewModel = this.mapper.Map<ProductViewModel>(newProduct);
                    return viewModel;
                }
            }

            return default;
        }

        public async Task<ProductViewModel> EditProductAsync(EditProductInputModel model)
        {
            if (model == null)
            {
                return default;
            }

            var product = await this.GetProductByIdAsync(model.Id);

            if (product == null)
            {
                return default;
            }

            if (model.ShoppingListIds != default)
            {
                var successfullyAdded = await this.AddProductToShoppingList(model);

                if (!successfullyAdded)
                {
                    return default;
                }
            }

            var updatedProduct = await this.entityRepository.EditEntityAsync(model, product);

            var viewModel = this.mapper.Map<ProductViewModel>(updatedProduct);
            return viewModel;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await this.dbContext.Products
                .OrderBy(x => x.Id)
                .ToListAsync();

            var viewModel = this.mapper.Map<IEnumerable<ProductViewModel>>(products);

            return viewModel;
        }

        public async Task<ProductWithCategoriesAndShoppingListsViewModel> GetProductWithCategoriesAndShoppingListsViewModelByIdAsync(int id)
        {
            var product = await this.GetProductByIdAsync(id);

            if (product == null)
            {
                return default;
            }

            var productViewModel = this.mapper.Map<ProductViewModel>(product);
            var categoriesViewModel = await this.categoryService.GetAllCategoriesAsync();
            var shoppingListsViewModel = await this.shoppingListService.GetAllShoppingLists();

            var productAndCategoriesViewModel = new ProductWithCategoriesAndShoppingListsViewModel
            {
                Product = productViewModel,
                CategoryCollection = categoriesViewModel,
                ShoppingListCollection = shoppingListsViewModel,
            };

            return productAndCategoriesViewModel;
        }

        private async Task<Product> GetProductByIdAsync(int id)
        {
            return await this.dbContext.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<bool> AddProductToShoppingList(EditProductInputModel model)
        {
            var addProductToShoppingListModel = this.mapper.Map<AddProductToShoppingListsInputModel>(model);
            return await this.shoppingListService.AddProductToShoppingListsAsync(addProductToShoppingListModel);
        }
    }
}
