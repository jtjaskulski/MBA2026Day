using MongoDB.Driver;
using SolutionOrders.Workers.Application.Abstractions;
using SolutionOrders.Workers.Application.Messages.DTOs;
using SolutionOrders.Workers.Persistence.Mongo;

namespace SolutionOrders.Workers.Persistence.Repositories;

/// <summary>MongoDB-backed reads for workers.</summary>
public sealed class WorkerMongoReadRepository(IMongoDatabase database) : IWorkerReadRepository
{
    private const string CollectionName = "workers";

    /// <inheritdoc />
    public async Task<IReadOnlyList<WorkerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await database.GetCollection<WorkerReadDocument>(CollectionName)
            .Find(x => x.IsActive)
            .SortBy(x => x.LastName)
            .ToCursorAsync(cancellationToken)
            .ConfigureAwait(false);

        var list = new List<WorkerDto>();
        while (await cursor.MoveNextAsync(cancellationToken).ConfigureAwait(false))
        {
            foreach (var doc in cursor.Current)
                list.Add(doc.ToDto());
        }

        return list;
    }

    /// <inheritdoc />
    public async Task<WorkerDto> GetByIdAsync(int idWorker, CancellationToken cancellationToken = default)
    {
        var doc = await database.GetCollection<WorkerReadDocument>(CollectionName)
            .Find(x => x.IdWorker == idWorker && x.IsActive)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (doc is null)
            throw new KeyNotFoundException($"Worker with ID {idWorker} not found");

        return doc.ToDto();
    }
}
