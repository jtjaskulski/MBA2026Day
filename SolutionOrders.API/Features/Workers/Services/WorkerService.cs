using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Workers.Services
{
    public class WorkerService(ApplicationDbContext context) : IWorkerService
    {
        public async Task CreateWorker(Worker worker, CancellationToken cancellationToken)
        {
            context.Workers.Add(worker);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
