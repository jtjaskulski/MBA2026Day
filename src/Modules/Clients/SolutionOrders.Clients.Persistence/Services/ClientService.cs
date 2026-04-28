using Microsoft.EntityFrameworkCore;
using SolutionOrders.Clients.Application.Services;
using SolutionOrders.Clients.Domain.Entities;

namespace SolutionOrders.Clients.Persistence.Services;

/// <summary>
/// Persists new clients to SQL Server (source of truth).
/// </summary>
public sealed class ClientService(DbContext context) : IClientService
{
    /// <inheritdoc />
    public async Task CreateClient(Client client, CancellationToken cancellationToken)
    {
        context.Set<Client>().Add(client);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
