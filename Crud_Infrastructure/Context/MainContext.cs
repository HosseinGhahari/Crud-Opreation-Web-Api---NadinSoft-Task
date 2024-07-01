using Crud_Domain;
using Crud_Opreation.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Crud_Opreation.Context
{
    // MainContext represents the database context for my application.
    // It provides access to the 'products' table through the DbSet<Product> property.
    // The OnModelCreating method is overridden to apply entity configurations.
    // Make sure to inject DbContextOptions<MainContext> in your constructor.
    public class MainContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public MainContext(DbContextOptions<MainContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
