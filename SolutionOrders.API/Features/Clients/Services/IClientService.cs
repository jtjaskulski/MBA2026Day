using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Clients.Services
{
    public interface IClientService
    {
        Task CreateClient(Client client, CancellationToken cancellationToken);
    }
}