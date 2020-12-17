using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineMenu.Application;

namespace OnlineMenu.Persistence
{
    public static class ServiceSettingsExtensions {
        public static void ConfigureDbContext(this IServiceCollection services, string conntectionString)
        {
            services
                .AddDbContext<IOnlineMenuContext, OnlineMenuContext>(options => options
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .UseSqlServer(conntectionString, x => x.MigrationsAssembly("OnlineMenu.Persistence"))
                );
        }

    }
}