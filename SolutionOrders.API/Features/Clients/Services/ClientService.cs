using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Clients.Services
{
    public class ClientService(ApplicationDbContext context) : IClientService
    {
        public async Task CreateClient(Client client, CancellationToken cancellationToken)
        {
            context.Clients.Add(client);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}