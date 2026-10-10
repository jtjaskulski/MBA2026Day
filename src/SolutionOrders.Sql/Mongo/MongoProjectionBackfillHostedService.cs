using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SolutionOrders.Sql.Data;

namespace SolutionOrders.Sql.Mongo;

/// <summary>
/// After SQL is migrated, rebuilds Mongo read collections from the authoritative SQL database (startup projection rebuild).
/// </summary>
public sealed class MongoProjectionBackfillHostedService(
    IServiceScopeFactory scopeFactory,
    ILogger<MongoProjectionBackfillHostedService> logger)
    : IHostedService
{
    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var projectionSync = scope.ServiceProvider.GetRequiredService<MongoProjectionSyncService>();

        await projectionSync.FullRebuildAsync(db, cancellationToken).ConfigureAwait(false);

        logger.LogInformation("Mongo projection full rebuild completed.");
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
