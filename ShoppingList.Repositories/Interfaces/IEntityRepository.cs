namespace ShoppingList.Repositories.Interfaces
{
    public interface IEntityRepository
    {
        Task<TEntity> CreateEntityAsync<TInput, TEntity>(TInput model) where TEntity : class;

        Task<TEntity> EditEntityAsync<TInput, TEntity>(TInput model, TEntity entity) where TEntity : class;
    }
}
