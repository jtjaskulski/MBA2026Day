using MongoDB.Driver;
using SolutionOrders.Clients.Application.Abstractions;
using SolutionOrders.Clients.Application.Messages.DTOs;
using SolutionOrders.Clients.Persistence.Mongo;

namespace SolutionOrders.Clients.Persistence.Repositories;

/// <summary>MongoDB-backed reads for clients.</summary>
public sealed class ClientMongoReadRepository(IMongoDatabase database) : IClientReadRepository
{
    private const string CollectionName = "clients";

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await database.GetCollection<ClientReadDocument>(CollectionName)
            .Find(x => x.IsActive)
            .SortBy(x => x.Name)
            .ToCursorAsync(cancellationToken)
            .ConfigureAwait(false);

        var list = new List<ClientDto>();
        while (await cursor.MoveNextAsync(cancellationToken).ConfigureAwait(false))
        {
            foreach (var doc in cursor.Current)
                list.Add(doc.ToDto());
        }

        return list;
    }

    /// <inheritdoc />
    public async Task<ClientDto> GetByIdAsync(int idClient, CancellationToken cancellationToken = default)
    {
        var doc = await database.GetCollection<ClientReadDocument>(CollectionName)
            .Find(x => x.IdClient == idClient && x.IsActive)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (doc is null)
            throw new KeyNotFoundException($"Client with ID {idClient} not found");

        return doc.ToDto();
    }
}
