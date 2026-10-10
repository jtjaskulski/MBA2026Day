using Microsoft.Extensions.DependencyInjection;
using SolutionOrders.Items.Application.Abstractions;
using SolutionOrders.Items.Application.Services;
using SolutionOrders.Items.Persistence.Repositories;
using SolutionOrders.Items.Persistence.Services;

namespace SolutionOrders.Items.Infrastructure;

/// <summary>
/// Registers Items module persistence implementations.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Items bounded-context services.
    /// </summary>
    public static IServiceCollection AddItemsModule(this IServiceCollection services)
    {
        services.AddScoped<IItemProvider, ItemProvider>();
        services.AddScoped<IItemReadRepository, ItemMongoReadRepository>();
        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}
