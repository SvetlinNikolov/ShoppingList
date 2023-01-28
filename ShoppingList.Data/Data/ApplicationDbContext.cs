using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShoppingList.Data.Models;
using System.Reflection.Emit;

namespace ShoppingList.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ShoppingList.Data.Models.ShoppingList> ShoppingLists { get; set; }

        public DbSet<ProductsBought> ProductsBought { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductsBought>().HasKey(x => new { x.ShoppingListId, x.ProductId });

            base.OnModelCreating(builder);
        }
    }
}