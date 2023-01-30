using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShoppingList.Data.Models;
using ShoppingList.Data.Seeding;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace ShoppingList.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ShoppingList.Data.Models.ShoppingList> ShoppingLists { get; set; }

        public DbSet<ShoppingListsProducts> ShoppingListsProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShoppingListsProducts>().HasKey(x => x.Id);

            builder.Seed();

            base.OnModelCreating(builder);
        }
    }
}