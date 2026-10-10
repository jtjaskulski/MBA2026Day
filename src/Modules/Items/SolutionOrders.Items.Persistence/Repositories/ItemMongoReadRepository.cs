using MongoDB.Driver;
using SolutionOrders.Items.Application.Abstractions;
using SolutionOrders.Items.Application.Messages.DTOs;
using SolutionOrders.Items.Persistence.Mongo;

namespace SolutionOrders.Items.Persistence.Repositories;

/// <summary>MongoDB-backed reads for catalog items.</summary>
public sealed class ItemMongoReadRepository(IMongoDatabase database) : IItemReadRepository
{
    private const string CollectionName = "items";

    /// <inheritdoc />
    public async Task<IReadOnlyList<ItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await database.GetCollection<ItemReadDocument>(CollectionName)
            .Find(x => x.IsActive)
            .SortBy(x => x.Name)
            .ToCursorAsync(cancellationToken)
            .ConfigureAwait(false);

        var list = new List<ItemDto>();
        while (await cursor.MoveNextAsync(cancellationToken).ConfigureAwait(false))
        {
            foreach (var doc in cursor.Current)
                list.Add(doc.ToDto());
        }

        return list;
    }

    /// <inheritdoc />
    public async Task<ItemDto> GetByIdAsync(int idItem, CancellationToken cancellationToken = default)
    {
        var doc = await database.GetCollection<ItemReadDocument>(CollectionName)
            .Find(x => x.IdItem == idItem && x.IsActive)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (doc is null)
            throw new KeyNotFoundException($"Produkt o ID {idItem} nie istnieje");

        return doc.ToDto();
    }
}
