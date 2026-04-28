using Microsoft.Extensions.DependencyInjection;
using SolutionOrders.Clients.Application.Abstractions;
using SolutionOrders.Clients.Application.Services;
using SolutionOrders.Clients.Persistence.Repositories;
using SolutionOrders.Clients.Persistence.Services;

namespace SolutionOrders.Clients.Infrastructure;

/// <summary>
/// Registers Clients module persistence implementations (SQL writes + legacy SQL reads for mutations).
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Clients bounded-context services.
    /// </summary>
    public static IServiceCollection AddClientsModule(this IServiceCollection services)
    {
        services.AddScoped<IClientProvider, ClientProvider>();
        services.AddScoped<IClientReadRepository, ClientMongoReadRepository>();
        services.AddScoped<IClientService, ClientService>();

        return services;
    }
}
