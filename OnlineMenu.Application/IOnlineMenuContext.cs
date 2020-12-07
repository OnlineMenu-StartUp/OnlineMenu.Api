using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain.Models;

namespace OnlineMenu.Application
{
    public interface IOnlineMenuContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderedProduct> OrderedProducts { get; set; }
        DbSet<OrderedTopping> OrderedProductExtras { get; set; }
        DbSet<PaymentType> PaymentTypes { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Topping> Toppings { get; set; }
        DbSet<ProductTopping> ProductToppings { get; set; }
        DbSet<Status> Statuses { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<Cook> Cooks { get; set; }
        DbSet<Customer> Customers { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
