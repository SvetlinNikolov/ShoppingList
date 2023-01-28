using AutoMapper;
using ShoppingList.Data;
using ShoppingList.Repositories.Interfaces;

namespace ShoppingList.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public EntityRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TEntity> CreateEntityAsync<TInput, TEntity>(TInput model)
            where TEntity : class
        {
            var newEntity = mapper.Map<TEntity>(model);

            dbContext.Set<TEntity>().Add(newEntity);
            await dbContext.SaveChangesAsync();

            return newEntity;
        }

        public async Task<TEntity> EditEntityAsync<TInput, TEntity>(TInput model, TEntity entity) where TEntity : class
        {
            this.mapper.Map(model, entity);

            dbContext.Set<TEntity>().Update(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
