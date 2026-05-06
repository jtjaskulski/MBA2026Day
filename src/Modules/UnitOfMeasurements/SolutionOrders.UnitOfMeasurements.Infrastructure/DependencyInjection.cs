using Microsoft.Extensions.DependencyInjection;
using SolutionOrders.UnitOfMeasurements.Application.Abstractions;
using SolutionOrders.UnitOfMeasurements.Persistence.Repositories;

namespace SolutionOrders.UnitOfMeasurements.Infrastructure;

/// <summary>
/// Registers UnitOfMeasurements module persistence implementations.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds UnitOfMeasurements bounded-context services.
    /// </summary>
    public static IServiceCollection AddUnitOfMeasurementsModule(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfMeasurementProvider, UnitOfMeasurementProvider>();
        services.AddScoped<IUnitOfMeasurementReadRepository, UnitOfMeasurementMongoReadRepository>();

        return services;
    }
}
