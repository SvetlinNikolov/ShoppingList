using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models.Categories;
using ShoppingList.Models.Products;
using ShoppingList.Repositories.Interfaces;
using ShoppingList.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEntityRepository entityRepository;

        public ProductService(ApplicationDbContext dbContext,
            IMapper mapper,
            IEntityRepository entityRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.entityRepository = entityRepository;
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

            var updatedProduct = this.entityRepository.EditEntityAsync(model, product);

            var viewModel = this.mapper.Map<ProductViewModel>(updatedProduct);
            return viewModel;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductViewModel> GetProductViewModelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Product> GetProductByIdAsync(int id)
        {
            return await this.dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
