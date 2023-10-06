using Domain.Adapters;

using Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDependency
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}