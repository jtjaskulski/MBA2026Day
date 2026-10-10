using Microsoft.Extensions.DependencyInjection;
using SolutionOrders.Categories.Application.Abstractions;
using SolutionOrders.Categories.Persistence.Repositories;

namespace SolutionOrders.Categories.Infrastructure;

/// <summary>
/// Registers Categories bounded-context services (Mongo read repositories).
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Categories module Application + Persistence implementations to the DI container.
    /// MediatR handlers are registered centrally from <c>Program.cs</c> via assembly scanning.
    /// </summary>
    public static IServiceCollection AddCategoriesModule(this IServiceCollection services)
    {
        services.AddScoped<ICategoryReadRepository, CategoryMongoReadRepository>();

        return services;
    }
}
