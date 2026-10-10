using Microsoft.Extensions.DependencyInjection;
using SolutionOrders.Workers.Application.Abstractions;
using SolutionOrders.Workers.Application.Services;
using SolutionOrders.Workers.Persistence.Repositories;
using SolutionOrders.Workers.Persistence.Services;

namespace SolutionOrders.Workers.Infrastructure;

/// <summary>
/// Registers Workers module persistence implementations.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Workers bounded-context services.
    /// </summary>
    public static IServiceCollection AddWorkersModule(this IServiceCollection services)
    {
        services.AddScoped<IWorkerProvider, WorkerProvider>();
        services.AddScoped<IWorkerReadRepository, WorkerMongoReadRepository>();
        services.AddScoped<IWorkerService, WorkerService>();

        return services;
    }
}
