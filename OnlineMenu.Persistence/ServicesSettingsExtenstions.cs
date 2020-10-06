using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineMenu.Application;
using OnlineMenu.Persistence;

public static class ServiceSettingsExtensions {
        public static void ConfigureDbContext(this IServiceCollection services, string conntectionString)
        {
            services
                .AddDbContext<IOnlineMenuContext, OnlineMenuContext>(options => 
                options.UseSqlServer(conntectionString));
        }

}