using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Workers.Services
{
    public interface IWorkerService
    {
        Task CreateWorker(Worker worker, CancellationToken cancellationToken);
    }
}
