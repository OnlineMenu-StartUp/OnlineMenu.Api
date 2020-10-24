using Microsoft.EntityFrameworkCore;
using OnlineMenu.Application;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Persistence
{
    public class OnlineMenuContext : DbContext, IOnlineMenuContext
    {
        public OnlineMenuContext(DbContextOptions<OnlineMenuContext> options): base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
        public DbSet<OrderedProductExtra> OrderedProductExtras { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductExtra> ProductExtras { get; set; }
        public DbSet<ProductProductExtra> ProductProductExtras { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cook> Cooks { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Status>()
                .HasIndex(s => s.Name)
                .IsUnique();
            
            builder.Entity<ProductProductExtra>()
                .HasKey(ppe => new { ppe.ProductId, ppe.ProductExtraId });
            
            builder.Entity<ProductProductExtra>()
                .HasOne<Product>(ppe => ppe.Product)
                .WithMany(p => p.ProductProductExtras)
                .HasForeignKey(ppe => ppe.ProductId);

            builder.Entity<ProductProductExtra>()
                .HasOne<ProductExtra>(ppe => ppe.ProductExtra)
                .WithMany(pe => pe.ProductProductExtras)
                .HasForeignKey(ppe => ppe.ProductExtraId);
            
            builder.Entity<Category>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
