using Microsoft.EntityFrameworkCore;
using OrdersDelivery.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace OrdersDelivery.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            modelBuilder.Entity<Order>().HasIndex(o => o.Id).IsUnique();
        }
    }
}
