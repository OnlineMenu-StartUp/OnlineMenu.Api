using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application
{
    public interface IOnlineMenuContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderedProduct> OrderedProducts { get; set; }
        DbSet<OrderedProductExtra> OrderedProductExtras { get; set; }
        DbSet<PaymentType> PaymentTypes { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductExtra> ProductExtras { get; set; }
        DbSet<ProductProductExtra> ProductProductExtras { get; set; }
        DbSet<Status> Statuses { get; set; }
        int SaveChanges();
    }
}
