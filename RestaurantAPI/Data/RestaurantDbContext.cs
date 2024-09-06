using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Food> Foods { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(c => c.customerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Food>().Property(f => f.foodId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Transaction>().HasOne(t => t.customer).WithMany(c => c.transactions).HasForeignKey(t => t.customerId);
            modelBuilder.Entity<Transaction>().HasOne(t => t.food).WithMany(f => f.transactions).HasForeignKey(t => t.foodId);
            modelBuilder.Entity<Transaction>().Property(t => t.transactionId).ValueGeneratedOnAdd();
        }
    }
}
