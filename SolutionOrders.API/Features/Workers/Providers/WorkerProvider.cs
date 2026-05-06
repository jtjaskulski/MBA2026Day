using Microsoft.EntityFrameworkCore;
using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Workers.Providers
{
    public class WorkerProvider(ApplicationDbContext context) : IWorkerProvider
    {
        public async Task<IEnumerable<Worker>> GetAllWorkersAsync(bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.Workers.Where(w => w.IsActive);

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.OrderBy(w => w.LastName).ToListAsync(cancellationToken);
        }

        public async Task<Worker> GetWorkerByIdAsync(int id, bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.Workers.Where(w => w.IsActive);

            if (asNoTracking)
                query = query.AsNoTracking();

            var worker = await query.FirstOrDefaultAsync(w => w.IdWorker == id, cancellationToken);
            return worker ?? throw new KeyNotFoundException($"Worker with ID {id} not found");
        }
    }
}