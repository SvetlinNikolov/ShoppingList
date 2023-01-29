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

        public ProductService(ApplicationDbContext dbContext,
            IMapper mapper,
            IEntityRepository entityRepository,
            ICategoryService categoryService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.entityRepository = entityRepository;
            this.categoryService = categoryService;
        }

        public async Task<ProductViewModel> CreateProductAsync(CreateProductInputModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Name))
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
            if (model is null)
            {
                return default;
            }

            var product = await this.GetProductByIdAsync(model.Id);

            if (product == null)
            {
                return default;
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

        public async Task<ProductWithCategoriesViewModel> GetProductWithCategoriesViewModelByIdAsync(int id)
        {
            var product = await this.GetProductByIdAsync(id);

            if (product == null)
            {
                return default;
            }

            var productViewModel = this.mapper.Map<ProductViewModel>(product);
            var categoriesViewModel = await this.categoryService.GetAllCategoriesAsync();

            var productAndCategoriesViewModel = new ProductWithCategoriesViewModel
            {
                Product = productViewModel,
                CategoryCollection = categoriesViewModel
            };

            return productAndCategoriesViewModel;
        }

        private async Task<Product> GetProductByIdAsync(int id)
        {
            return await this.dbContext.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
