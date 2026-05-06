using Microsoft.Extensions.DependencyInjection;
using SolutionOrders.Orders.Application.Abstractions;
using SolutionOrders.Orders.Application.Services;
using SolutionOrders.Orders.Persistence.Repositories;
using SolutionOrders.Orders.Persistence.Services;

namespace SolutionOrders.Orders.Infrastructure;

/// <summary>
/// Registers Orders module persistence implementations.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Orders bounded-context services.
    /// </summary>
    public static IServiceCollection AddOrdersModule(this IServiceCollection services)
    {
        services.AddScoped<IOrderProvider, OrderProvider>();
        services.AddScoped<IOrderReadRepository, OrderMongoReadRepository>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
