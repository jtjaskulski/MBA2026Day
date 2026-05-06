using SolutionOrders.Clients.Domain.Entities;

namespace SolutionOrders.Clients.Application.Services
{
    public interface IClientService
    {
        Task CreateClient(Client client, CancellationToken cancellationToken);
    }
}


