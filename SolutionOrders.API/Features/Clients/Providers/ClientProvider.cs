using Microsoft.EntityFrameworkCore;
using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Clients.Providers
{
    public class ClientProvider(ApplicationDbContext context) : IClientProvider
    {
        public async Task<IEnumerable<Client>> GetAllClientsAsync(bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.Clients.Where(c => c.IsActive);

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.OrderBy(c => c.Name).ToListAsync(cancellationToken);
        }

        public async Task<Client> GetClientByIdAsync(int id, bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.Clients.Where(c => c.IsActive);

            if (asNoTracking)
                query = query.AsNoTracking();

            var client = await query.FirstOrDefaultAsync(c => c.IdClient == id, cancellationToken);
            return client ?? throw new KeyNotFoundException($"Client with ID {id} not found");
        }
    }
}