using Microsoft.EntityFrameworkCore;
using OnlineMenu.Application;
using OnlineMenu.Domain;

namespace OnlineMenu.Persistence
{
    public class OnlineMenuContext: DbContext, IOnlineMenuContext
    {
        public OnlineMenuContext(DbContextOptions<OnlineMenuContext> options): base(options)
        { }
        public DbSet<Order> Orders { get; set; }
    }
}
