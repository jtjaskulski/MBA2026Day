using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SolutionOrders.Categories.Domain.Entities;
using SolutionOrders.Categories.Persistence.Mongo;
using SolutionOrders.Clients.Domain.Entities;
using SolutionOrders.Clients.Persistence.Mongo;
using SolutionOrders.Items.Domain.Entities;
using SolutionOrders.Items.Persistence.Mongo;
using SolutionOrders.Orders.Domain.Entities;
using SolutionOrders.Orders.Persistence.Mongo;
using SolutionOrders.Sql.Data;
using SolutionOrders.UnitOfMeasurements.Domain.Entities;
using SolutionOrders.UnitOfMeasurements.Persistence.Mongo;
using SolutionOrders.Workers.Domain.Entities;
using SolutionOrders.Workers.Persistence.Mongo;

namespace SolutionOrders.Sql.Mongo;

/// <summary>
/// Keeps MongoDB read collections in sync with SQL (writes remain authoritative on SQL Server).
/// </summary>
public sealed class MongoProjectionSyncService(IMongoDatabase mongo)
{
    private static readonly ReplaceOptions UpsertReplace = new() { IsUpsert = true };

    /// <summary>Rebuild all projections from SQL (startup backfill + orphan cleanup).</summary>
    public async Task FullRebuildAsync(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        await SyncCategoriesFullAsync(db, cancellationToken).ConfigureAwait(false);
        await SyncClientsFullAsync(db, cancellationToken).ConfigureAwait(false);
        await SyncWorkersFullAsync(db, cancellationToken).ConfigureAwait(false);
        await SyncUnitsFullAsync(db, cancellationToken).ConfigureAwait(false);
        await SyncItemsFullAsync(db, cancellationToken).ConfigureAwait(false);
        await SyncOrdersFullAsync(db, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Incremental upserts/deletes after SQL persistence succeeds.</summary>
    internal async Task IncrementalSyncAsync(ApplicationDbContext db, PendingMongoSyncBag bag,
        CancellationToken cancellationToken)
    {
        if (!bag.HasPendingWork)
            return;

        await ExpandRelatedOrderIdsAsync(db, bag, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.DeletedCategoryIds)
            await mongo.GetCollection<CategoryReadDocument>(MongoCollectionNames.Categories)
                .DeleteOneAsync(x => x.IdCategory == id, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.DeletedOrderIds)
            await mongo.GetCollection<OrderReadDocument>(MongoCollectionNames.Orders)
                .DeleteOneAsync(x => x.IdOrder == id, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.CategoryIds.Where(i => !bag.DeletedCategoryIds.Contains(i)))
            await UpsertCategoryAsync(db, id, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.ClientIds)
            await UpsertClientAsync(db, id, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.WorkerIds)
            await UpsertWorkerAsync(db, id, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.UnitOfMeasurementIds)
            await UpsertUnitAsync(db, id, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.ItemIds)
            await UpsertItemAsync(db, id, cancellationToken).ConfigureAwait(false);

        foreach (var id in bag.OrderIds.Where(i => !bag.DeletedOrderIds.Contains(i)))
            await UpsertOrderAsync(db, id, cancellationToken).ConfigureAwait(false);
    }

    private async Task ExpandRelatedOrderIdsAsync(ApplicationDbContext db, PendingMongoSyncBag bag,
        CancellationToken cancellationToken)
    {
        if (bag.ClientIds.Count > 0)
        {
            var ids = await db.Set<Order>()
                .AsNoTracking()
                .Where(o => o.IdClient != null && bag.ClientIds.Contains(o.IdClient!.Value))
                .Select(o => o.IdOrder)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            foreach (var id in ids)
                bag.OrderIds.Add(id);
        }

        if (bag.WorkerIds.Count > 0)
        {
            var ids = await db.Set<Order>()
                .AsNoTracking()
                .Where(o => o.IdWorker != null && bag.WorkerIds.Contains(o.IdWorker!.Value))
                .Select(o => o.IdOrder)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            foreach (var id in ids)
                bag.OrderIds.Add(id);
        }

        if (bag.ItemIds.Count > 0)
        {
            var ids = await db.Set<OrderItem>()
                .AsNoTracking()
                .Where(oi => bag.ItemIds.Contains(oi.IdItem))
                .Select(oi => oi.IdOrder)
                .Distinct()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            foreach (var id in ids)
                bag.OrderIds.Add(id);
        }
    }

    private async Task SyncCategoriesFullAsync(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        var sqlIds = await db.Set<Category>().AsNoTracking().Select(c => c.IdCategory).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var coll = mongo.GetCollection<CategoryReadDocument>(MongoCollectionNames.Categories);
        await coll.DeleteManyAsync(x => !sqlIds.Contains(x.IdCategory), cancellationToken).ConfigureAwait(false);

        foreach (var id in sqlIds)
            await UpsertCategoryAsync(db, id, cancellationToken).ConfigureAwait(false);
    }

    private async Task SyncClientsFullAsync(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        var sqlIds = await db.Set<Client>().AsNoTracking().Select(c => c.IdClient).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var coll = mongo.GetCollection<ClientReadDocument>(MongoCollectionNames.Clients);
        await coll.DeleteManyAsync(x => !sqlIds.Contains(x.IdClient), cancellationToken).ConfigureAwait(false);

        foreach (var id in sqlIds)
            await UpsertClientAsync(db, id, cancellationToken).ConfigureAwait(false);
    }

    private async Task SyncWorkersFullAsync(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        var sqlIds = await db.Set<Worker>().AsNoTracking().Select(w => w.IdWorker).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var coll = mongo.GetCollection<WorkerReadDocument>(MongoCollectionNames.Workers);
        await coll.DeleteManyAsync(x => !sqlIds.Contains(x.IdWorker), cancellationToken).ConfigureAwait(false);

        foreach (var id in sqlIds)
            await UpsertWorkerAsync(db, id, cancellationToken).ConfigureAwait(false);
    }

    private async Task SyncUnitsFullAsync(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        var sqlIds = await db.Set<UnitOfMeasurement>().AsNoTracking().Select(u => u.IdUnitOfMeasurement)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        var coll = mongo.GetCollection<UnitOfMeasurementReadDocument>(MongoCollectionNames.UnitOfMeasurements);
        await coll.DeleteManyAsync(x => !sqlIds.Contains(x.IdUnitOfMeasurement), cancellationToken)
            .ConfigureAwait(false);

        foreach (var id in sqlIds)
            await UpsertUnitAsync(db, id, cancellationToken).ConfigureAwait(false);
    }

    private async Task SyncItemsFullAsync(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        var sqlIds = await db.Set<Item>().AsNoTracking().Select(i => i.IdItem).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var coll = mongo.GetCollection<ItemReadDocument>(MongoCollectionNames.Items);
        await coll.DeleteManyAsync(x => !sqlIds.Contains(x.IdItem), cancellationToken).ConfigureAwait(false);

        foreach (var id in sqlIds)
            await UpsertItemAsync(db, id, cancellationToken).ConfigureAwait(false);
    }

    private async Task SyncOrdersFullAsync(ApplicationDbContext db, CancellationToken cancellationToken)
    {
        var sqlIds = await db.Set<Order>().AsNoTracking().Select(o => o.IdOrder).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var coll = mongo.GetCollection<OrderReadDocument>(MongoCollectionNames.Orders);
        await coll.DeleteManyAsync(x => !sqlIds.Contains(x.IdOrder), cancellationToken).ConfigureAwait(false);

        foreach (var id in sqlIds)
            await UpsertOrderAsync(db, id, cancellationToken).ConfigureAwait(false);
    }

    private async Task UpsertCategoryAsync(ApplicationDbContext db, int id, CancellationToken cancellationToken)
    {
        var entity = await db.Set<Category>().AsNoTracking().FirstOrDefaultAsync(c => c.IdCategory == id,
            cancellationToken).ConfigureAwait(false);
        if (entity is null)
            return;

        var doc = new CategoryReadDocument
        {
            IdCategory = entity.IdCategory,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive
        };

        await mongo.GetCollection<CategoryReadDocument>(MongoCollectionNames.Categories).ReplaceOneAsync(
                x => x.IdCategory == doc.IdCategory,
                doc,
                UpsertReplace,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task UpsertClientAsync(ApplicationDbContext db, int id, CancellationToken cancellationToken)
    {
        var entity = await db.Set<Client>().AsNoTracking().FirstOrDefaultAsync(c => c.IdClient == id,
            cancellationToken).ConfigureAwait(false);
        if (entity is null)
            return;

        var doc = new ClientReadDocument
        {
            IdClient = entity.IdClient,
            Name = entity.Name,
            Address = entity.Address,
            PhoneNumber = entity.PhoneNumber,
            IsActive = entity.IsActive
        };

        await mongo.GetCollection<ClientReadDocument>(MongoCollectionNames.Clients).ReplaceOneAsync(
                x => x.IdClient == doc.IdClient,
                doc,
                UpsertReplace,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task UpsertWorkerAsync(ApplicationDbContext db, int id, CancellationToken cancellationToken)
    {
        var entity = await db.Set<Worker>().AsNoTracking().FirstOrDefaultAsync(w => w.IdWorker == id,
            cancellationToken).ConfigureAwait(false);
        if (entity is null)
            return;

        var doc = new WorkerReadDocument
        {
            IdWorker = entity.IdWorker,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Login = entity.Login,
            IsActive = entity.IsActive
        };

        await mongo.GetCollection<WorkerReadDocument>(MongoCollectionNames.Workers).ReplaceOneAsync(
                x => x.IdWorker == doc.IdWorker,
                doc,
                UpsertReplace,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task UpsertUnitAsync(ApplicationDbContext db, int id, CancellationToken cancellationToken)
    {
        var entity = await db.Set<UnitOfMeasurement>().AsNoTracking()
            .FirstOrDefaultAsync(u => u.IdUnitOfMeasurement == id, cancellationToken).ConfigureAwait(false);
        if (entity is null)
            return;

        var doc = new UnitOfMeasurementReadDocument
        {
            IdUnitOfMeasurement = entity.IdUnitOfMeasurement,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive
        };

        await mongo.GetCollection<UnitOfMeasurementReadDocument>(MongoCollectionNames.UnitOfMeasurements)
            .ReplaceOneAsync(
                x => x.IdUnitOfMeasurement == doc.IdUnitOfMeasurement,
                doc,
                UpsertReplace,
                cancellationToken).ConfigureAwait(false);
    }

    private async Task UpsertItemAsync(ApplicationDbContext db, int id, CancellationToken cancellationToken)
    {
        var entity = await db.Set<Item>()
            .AsNoTracking()
            .Include(i => i.Category)
            .Include(i => i.UnitOfMeasurement)
            .FirstOrDefaultAsync(i => i.IdItem == id, cancellationToken)
            .ConfigureAwait(false);
        if (entity is null)
            return;

        var doc = new ItemReadDocument
        {
            IdItem = entity.IdItem,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Quantity = entity.Quantity,
            IdCategory = entity.IdCategory,
            CategoryName = entity.Category?.Name,
            IdUnitOfMeasurement = entity.IdUnitOfMeasurement,
            UnitName = entity.UnitOfMeasurement?.Name,
            Code = entity.Code,
            IsActive = entity.IsActive
        };

        await mongo.GetCollection<ItemReadDocument>(MongoCollectionNames.Items).ReplaceOneAsync(
                x => x.IdItem == doc.IdItem,
                doc,
                UpsertReplace,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task UpsertOrderAsync(ApplicationDbContext db, int id, CancellationToken cancellationToken)
    {
        var entity = await db.Set<Order>()
            .AsNoTracking()
            .Include(o => o.Client)
            .Include(o => o.Worker)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Item)
            .FirstOrDefaultAsync(o => o.IdOrder == id, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
            return;

        var doc = MapOrder(entity);

        await mongo.GetCollection<OrderReadDocument>(MongoCollectionNames.Orders).ReplaceOneAsync(
                x => x.IdOrder == doc.IdOrder,
                doc,
                UpsertReplace,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private static OrderReadDocument MapOrder(Order o)
    {
        var workerName = o.Worker != null ? $"{o.Worker.FirstName} {o.Worker.LastName}".Trim() : null;

        return new OrderReadDocument
        {
            IdOrder = o.IdOrder,
            DataOrder = o.DataOrder,
            IdClient = o.IdClient,
            ClientName = o.Client?.Name,
            IdWorker = o.IdWorker,
            WorkerName = string.IsNullOrWhiteSpace(workerName) ? null : workerName,
            Notes = o.Notes,
            DeliveryDate = o.DeliveryDate,
            OrderItems = o.OrderItems.OrderBy(oi => oi.IdOrderItem).Select(oi => new OrderItemReadDocument
            {
                IdOrderItem = oi.IdOrderItem,
                IdItem = oi.IdItem,
                ItemName = oi.Item?.Name,
                Quantity = oi.Quantity,
                Price = oi.Item?.Price,
                IsActive = oi.IsActive
            }).ToList()
        };
    }
}

internal static class MongoCollectionNames
{
    internal const string Categories = "categories";
    internal const string Clients = "clients";
    internal const string Workers = "workers";
    internal const string UnitOfMeasurements = "unitOfMeasurements";
    internal const string Items = "items";
    internal const string Orders = "orders";
}
