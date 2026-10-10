using Microsoft.EntityFrameworkCore;
using SolutionOrders.Categories.Domain.Entities;
using SolutionOrders.Clients.Domain.Entities;
using SolutionOrders.Items.Domain.Entities;
using SolutionOrders.Orders.Domain.Entities;
using SolutionOrders.UnitOfMeasurements.Domain.Entities;
using SolutionOrders.Workers.Domain.Entities;
using SolutionOrders.Sql.Data;

namespace SolutionOrders.Sql.Mongo;

/// <summary>
/// Tracks EF entities touched by the current save cycle so Mongo projections can be updated incrementally.
/// </summary>
internal sealed class PendingMongoSyncBag
{
    internal HashSet<int> CategoryIds { get; } = [];

    internal HashSet<int> DeletedCategoryIds { get; } = [];

    internal HashSet<int> ClientIds { get; } = [];

    internal HashSet<int> WorkerIds { get; } = [];

    internal HashSet<int> UnitOfMeasurementIds { get; } = [];

    internal HashSet<int> ItemIds { get; } = [];

    internal HashSet<int> OrderIds { get; } = [];

    internal HashSet<int> DeletedOrderIds { get; } = [];

    internal bool HasPendingWork =>
        CategoryIds.Count > 0
        || DeletedCategoryIds.Count > 0
        || ClientIds.Count > 0
        || WorkerIds.Count > 0
        || UnitOfMeasurementIds.Count > 0
        || ItemIds.Count > 0
        || OrderIds.Count > 0
        || DeletedOrderIds.Count > 0;

    internal static void CollectFromChangeTracker(ApplicationDbContext ctx, PendingMongoSyncBag bag)
    {
        foreach (var entry in ctx.ChangeTracker.Entries())
        {
            if (entry.State is EntityState.Unchanged or EntityState.Detached)
                continue;

            switch (entry.Entity)
            {
                case Category e:
                    bag.CategoryIds.Add(e.IdCategory);
                    if (entry.State == EntityState.Deleted)
                        bag.DeletedCategoryIds.Add(e.IdCategory);
                    break;

                case Client e:
                    bag.ClientIds.Add(e.IdClient);
                    break;

                case Worker e:
                    bag.WorkerIds.Add(e.IdWorker);
                    break;

                case UnitOfMeasurement e:
                    bag.UnitOfMeasurementIds.Add(e.IdUnitOfMeasurement);
                    break;

                case Item e:
                    bag.ItemIds.Add(e.IdItem);
                    break;

                case Order e:
                    bag.OrderIds.Add(e.IdOrder);
                    if (entry.State == EntityState.Deleted)
                        bag.DeletedOrderIds.Add(e.IdOrder);
                    break;

                case OrderItem e:
                    bag.OrderIds.Add(e.IdOrder);
                    break;
            }
        }
    }
}
