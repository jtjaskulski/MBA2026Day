using MongoDB.Driver;
using SolutionOrders.Categories.Application.Abstractions;
using SolutionOrders.Categories.Application.Messages.DTOs;
using SolutionOrders.Categories.Persistence.Mongo;

namespace SolutionOrders.Categories.Persistence.Repositories;

/// <summary>
/// MongoDB-backed implementation for category reads.
/// </summary>
public sealed class CategoryMongoReadRepository(IMongoDatabase database) : ICategoryReadRepository
{
    private const string CollectionName = "categories";

    /// <inheritdoc />
    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await database.GetCollection<CategoryReadDocument>(CollectionName)
            .Find(FilterDefinition<CategoryReadDocument>.Empty)
            .SortBy(x => x.IdCategory)
            .ToCursorAsync(cancellationToken)
            .ConfigureAwait(false);

        var list = new List<CategoryDto>();
        while (await cursor.MoveNextAsync(cancellationToken).ConfigureAwait(false))
        {
            foreach (var doc in cursor.Current)
                list.Add(doc.ToDto());
        }

        return list;
    }
}
