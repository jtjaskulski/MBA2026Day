using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Clients.Providers
{
    public interface IClientProvider
    {
        Task<IEnumerable<Client>> GetAllClientsAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
        Task<Client> GetClientByIdAsync(int id, bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}
