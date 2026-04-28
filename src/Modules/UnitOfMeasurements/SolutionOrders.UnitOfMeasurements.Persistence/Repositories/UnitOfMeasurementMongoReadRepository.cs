using MongoDB.Driver;
using SolutionOrders.UnitOfMeasurements.Application.Abstractions;
using SolutionOrders.UnitOfMeasurements.Application.Messages.DTOs;
using SolutionOrders.UnitOfMeasurements.Persistence.Mongo;

namespace SolutionOrders.UnitOfMeasurements.Persistence.Repositories;

/// <summary>MongoDB-backed reads for units of measurement.</summary>
public sealed class UnitOfMeasurementMongoReadRepository(IMongoDatabase database) : IUnitOfMeasurementReadRepository
{
    private const string CollectionName = "unitOfMeasurements";

    /// <inheritdoc />
    public async Task<IReadOnlyList<UnitOfMeasurementDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await database.GetCollection<UnitOfMeasurementReadDocument>(CollectionName)
            .Find(x => x.IsActive)
            .SortBy(x => x.Name)
            .ToCursorAsync(cancellationToken)
            .ConfigureAwait(false);

        var list = new List<UnitOfMeasurementDto>();
        while (await cursor.MoveNextAsync(cancellationToken).ConfigureAwait(false))
        {
            foreach (var doc in cursor.Current)
                list.Add(doc.ToDto());
        }

        return list;
    }
}
