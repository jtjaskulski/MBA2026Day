using Microsoft.EntityFrameworkCore;
using SolutionOrders.Workers.Application.Services;
using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Workers.Persistence.Services;

/// <inheritdoc cref="IWorkerService" />
public sealed class WorkerService(DbContext context) : IWorkerService
{
    /// <inheritdoc />
    public async Task CreateWorker(Worker worker, CancellationToken cancellationToken)
    {
        context.Set<Worker>().Add(worker);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
