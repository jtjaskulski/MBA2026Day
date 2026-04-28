using Microsoft.EntityFrameworkCore;
using SolutionOrders.Workers.Application.Abstractions;
using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Workers.Persistence.Repositories;

/// <inheritdoc cref="IWorkerProvider" />
public sealed class WorkerProvider(DbContext context) : IWorkerProvider
{
    /// <inheritdoc />
    public async Task<IEnumerable<Worker>> GetAllWorkersAsync(bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Worker>().Where(w => w.IsActive);

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.OrderBy(w => w.LastName).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Worker> GetWorkerByIdAsync(int id, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Worker>().Where(w => w.IsActive);

        if (asNoTracking)
            query = query.AsNoTracking();

        var worker = await query.FirstOrDefaultAsync(w => w.IdWorker == id, cancellationToken).ConfigureAwait(false);
        return worker ?? throw new KeyNotFoundException($"Worker with ID {id} not found");
    }
}
