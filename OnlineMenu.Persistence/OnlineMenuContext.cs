using System;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Application;
using OnlineMenu.Domain;

namespace OnlineMenu.Persistence
{
    public class OnlineMenuContext : DbContext, IOnlineMenuContext
    {
        public OnlineMenuContext(DbContextOptions<OnlineMenuContext> options): base(options)
        { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Status>()
                .HasIndex(s => s.Name)
                .IsUnique();
        }
    }
}
