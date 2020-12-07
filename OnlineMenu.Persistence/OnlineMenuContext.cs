using Microsoft.EntityFrameworkCore;
using OnlineMenu.Application;
using OnlineMenu.Domain.Models;
// ReSharper disable RedundantTypeArgumentsOfMethod

namespace OnlineMenu.Persistence
{
    public class OnlineMenuContext: DbContext, IOnlineMenuContext
    {
        public OnlineMenuContext(DbContextOptions<OnlineMenuContext> options): base(options)
        { }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderedProduct> OrderedProducts { get; set; } = null!;
        public DbSet<OrderedTopping> OrderedProductExtras { get; set; } = null!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Topping> Toppings { get; set; } = null!;
        public DbSet<ProductTopping> ProductToppings { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Cook> Cooks { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Status>()
                .HasIndex(s => s.Name)
                .IsUnique();
            
            builder.Entity<ProductTopping>()
                .HasKey(ppe => new { ppe.ProductId, ppe.ToppingId });
            
            builder.Entity<ProductTopping>()
                .HasOne<Product>(ppe => ppe.Product)
                .WithMany(p => p.ToppingLinks)
                .HasForeignKey(ppe => ppe.ProductId);

            builder.Entity<ProductTopping>()
                .HasOne<Topping>(ppe => ppe.Topping)
                .WithMany(pe => pe.ProductLink)
                .HasForeignKey(ppe => ppe.ToppingId);
            
            builder.Entity<Category>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
