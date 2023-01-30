using Microsoft.EntityFrameworkCore;
using ShoppingList.Data.Models;

namespace ShoppingList.Data.Seeding
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "TEST",
                },
                 new Category
                 {
                     Id = 2,
                     Name = "TEST2",
                 }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "TEST", CategoryId = 1 },
                    new Product { Id = 2, Name = "TEST2", CategoryId = 2 }
            );
        }
    }
}
