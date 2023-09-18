using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence.Migrations;

namespace Persistence
{
    public static class PersistenceDependency
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppDbCtrlEat")));
            services.AddHostedService<MigratorService>();

            return services;
        }
    }
}