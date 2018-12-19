using Microsoft.EntityFrameworkCore;
using Netcorewebapi.DataAccess.Data.Entities;

namespace Netcorewebapi.DataAccess.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 5)");
            base.OnModelCreating(modelBuilder);
        }
    }
}
