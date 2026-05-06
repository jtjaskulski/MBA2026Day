using SolutionOrders.Clients.Application.Messages.DTOs;

namespace SolutionOrders.Clients.Application.Abstractions;

/// <summary>
/// Read-only listing/detail for clients backed by MongoDB projections (writes remain on SQL).
/// </summary>
public interface IClientReadRepository
{
    /// <summary>Returns active clients sorted by name.</summary>
    Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Returns one active client or throws <see cref="KeyNotFoundException"/>.</summary>
    Task<ClientDto> GetByIdAsync(int idClient, CancellationToken cancellationToken = default);
}
