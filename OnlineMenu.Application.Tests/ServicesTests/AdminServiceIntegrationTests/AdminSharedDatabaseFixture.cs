using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineMenu.Persistence;

namespace OnlineMenu.Application.Tests.ServicesTests.AdminServiceIntegrationTests
{
    public class AdminSharedDatabaseFixture: IDisposable
    {
        private static readonly object LockObject = new object(); 
        private static bool isDatabaseInitialized;
    
        public AdminSharedDatabaseFixture()
        {
            Connection = new SqlConnection("Server = 161.35.208.211; Database=OnlineMenuTest2; User Id=sa; Password=caraC@ctus47; Persist Security Info=true");

            Seed();
        
            Connection.Open();
        }

        public DbConnection Connection { get; }
    
        public OnlineMenuContext CreateContext(DbTransaction transaction = null)
        {
            var context = new OnlineMenuContext(
                new DbContextOptionsBuilder<OnlineMenuContext>().UseSqlServer(Connection)
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .Options
                );

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }
        
            return context;
        }
    
        private void Seed()
        {
            lock (LockObject)
            {
                if (isDatabaseInitialized) return;

                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.Admins.Add(DummyData.Admin);

                    context.SaveChanges();
                }

                isDatabaseInitialized = true;
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}