using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Workers.Application.Abstractions;

/// <summary>
/// Worker queries backed by SQL for tracked reads used by commands.
/// </summary>
public interface IWorkerProvider
{
    Task<IEnumerable<Worker>> GetAllWorkersAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<Worker> GetWorkerByIdAsync(int id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}

