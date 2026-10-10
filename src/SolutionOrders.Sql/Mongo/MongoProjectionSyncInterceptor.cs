using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SolutionOrders.Sql.Data;

namespace SolutionOrders.Sql.Mongo;

/// <summary>
/// After SQL commits successfully, pushes affected rows into MongoDB read collections.
/// </summary>
public sealed class MongoProjectionSyncInterceptor(
    MongoProjectionSyncService sync,
    ILogger<MongoProjectionSyncInterceptor> logger) : SaveChangesInterceptor
{
    /// <inheritdoc />
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is ApplicationDbContext adb)
        {
            adb.PendingMongoProjectionSync = new PendingMongoSyncBag();
            PendingMongoSyncBag.CollectFromChangeTracker(adb, adb.PendingMongoProjectionSync);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is ApplicationDbContext adb && adb.PendingMongoProjectionSync is { } bag)
        {
            adb.PendingMongoProjectionSync = null;

            if (!bag.HasPendingWork)
                return await base.SavedChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);

            try
            {
                await sync.IncrementalSyncAsync(adb, bag, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex,
                    "Mongo projection incremental sync failed after SQL commit; read models may be stale until next write or restart.");
            }
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken).ConfigureAwait(false);
    }
}
