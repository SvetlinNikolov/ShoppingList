using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Data.Models;
using ShoppingList.Models.Categories;
using ShoppingList.Repositories.Interfaces;
using ShoppingList.Services.Interfaces;

namespace ShoppingList.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IEntityRepository entityRepository;

        public CategoryService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IEntityRepository entityRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.entityRepository = entityRepository;
        }

        public async Task<CategoryViewModel> CreateCategoryAsync(CreateCategoryInputModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Name))
            {
                var categoryInDb = await this.dbContext.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);

                if (categoryInDb == null)
                {
                    var newCategory = await this.entityRepository.CreateEntityAsync<CreateCategoryInputModel, Category>(model);

                    var viewModel = this.mapper.Map<CategoryViewModel>(newCategory);
                    return viewModel;
                }
            }

            return default;
        }

        public async Task<CategoryViewModel> GetCategoryViewModelByIdAsync(int id)
        {
            var category = await this.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return default;
            }

            var viewModel = this.mapper.Map<CategoryViewModel>(category);
            return viewModel;
        }

        public async Task<CategoryViewModel> EditCategoryAsync(EditCategoryInputModel model)
        {
            if (model is null)
            {
                return default;
            }

            var category = await this.GetCategoryByIdAsync(model.Id);

            if (category == null)
            {
                return default;
            }

            var updatedCategory = await this.entityRepository.EditEntityAsync(model, category);

            var viewModel = this.mapper.Map<CategoryViewModel>(updatedCategory);
            return viewModel;
        }

        public async Task<CategoryCollectionViewModel> GetAllCategoriesAsync()
        {
            var categories = await this.dbContext.Categories
                .OrderBy(x => x.Id)
                .ToListAsync();

            var categoryViewModels = this.mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            var viewModel = new CategoryCollectionViewModel
            {
                Categories = categoryViewModels
            };

            return viewModel;
        }

        private async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await this.dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
