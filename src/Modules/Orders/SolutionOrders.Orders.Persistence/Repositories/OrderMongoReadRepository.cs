using MongoDB.Driver;
using SolutionOrders.Orders.Application.Abstractions;
using SolutionOrders.Orders.Application.Messages.DTOs;
using SolutionOrders.Orders.Persistence.Mongo;

namespace SolutionOrders.Orders.Persistence.Repositories;

/// <summary>MongoDB-backed reads for orders.</summary>
public sealed class OrderMongoReadRepository(IMongoDatabase database) : IOrderReadRepository
{
    private const string CollectionName = "orders";

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await database.GetCollection<OrderReadDocument>(CollectionName)
            .Find(FilterDefinition<OrderReadDocument>.Empty)
            .SortByDescending(x => x.DataOrder)
            .ToCursorAsync(cancellationToken)
            .ConfigureAwait(false);

        var list = new List<OrderDto>();
        while (await cursor.MoveNextAsync(cancellationToken).ConfigureAwait(false))
        {
            foreach (var doc in cursor.Current)
                list.Add(doc.ToDto());
        }

        return list;
    }

    /// <inheritdoc />
    public async Task<OrderDto> GetByIdAsync(int idOrder, CancellationToken cancellationToken = default)
    {
        var doc = await database.GetCollection<OrderReadDocument>(CollectionName)
            .Find(x => x.IdOrder == idOrder)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (doc is null)
            throw new KeyNotFoundException($"Order with ID {idOrder} not found");

        return doc.ToDto();
    }
}
