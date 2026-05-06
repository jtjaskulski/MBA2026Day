using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Workers.Application.Services
{
    public interface IWorkerService
    {
        Task CreateWorker(Worker worker, CancellationToken cancellationToken);
    }
}

