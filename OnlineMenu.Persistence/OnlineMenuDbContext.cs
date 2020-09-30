using System;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Domain;

namespace OnlineMenu.Persistence
{
    public class OnlineMenuDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItems> OrderedItems { get; set; }
        public DbSet<OrderedOptionals> OrderedOptionals { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemOptional> ItemOptionals { get; set; }
        public DbSet<Optional> Optionals { get; set; }

        public OnlineMenuDbContext(DbContextOptions<OnlineMenuDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemOptional>()
                .HasKey(io => new { io.ItemId, io.OptionalId });



        }
    }
}
