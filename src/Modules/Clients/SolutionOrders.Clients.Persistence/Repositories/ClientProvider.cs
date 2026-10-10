using Microsoft.EntityFrameworkCore;
using SolutionOrders.Clients.Application.Abstractions;
using SolutionOrders.Clients.Domain.Entities;

namespace SolutionOrders.Clients.Persistence.Repositories;

/// <summary>
/// SQL-backed queries for clients used by commands that mutate EF-tracked entities (reads will move fully to Mongo projections).
/// </summary>
public sealed class ClientProvider(DbContext context) : IClientProvider
{
    /// <inheritdoc />
    public async Task<IEnumerable<Client>> GetAllClientsAsync(bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Client>().Where(c => c.IsActive);

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.OrderBy(c => c.Name).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Client> GetClientByIdAsync(int id, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Client>().Where(c => c.IsActive);

        if (asNoTracking)
            query = query.AsNoTracking();

        var client = await query.FirstOrDefaultAsync(c => c.IdClient == id, cancellationToken).ConfigureAwait(false);
        return client ?? throw new KeyNotFoundException($"Client with ID {id} not found");
    }
}
