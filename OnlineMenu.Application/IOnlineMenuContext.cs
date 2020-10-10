using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain;

namespace OnlineMenu.Application
{
    public interface IOnlineMenuContext
    {
        DbSet<Order> Orders { get; set; }
    }
}
