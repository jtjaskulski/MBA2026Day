using SolutionOrders.Clients.Domain.Entities;

namespace SolutionOrders.Clients.Application.Abstractions;

/// <summary>
/// Legacy SQL-backed client queries retained for commands that require tracked entities.
/// </summary>
public interface IClientProvider
{
    Task<IEnumerable<Client>> GetAllClientsAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<Client> GetClientByIdAsync(int id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}

