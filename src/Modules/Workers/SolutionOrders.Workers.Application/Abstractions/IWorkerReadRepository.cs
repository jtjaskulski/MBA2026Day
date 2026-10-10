using SolutionOrders.Workers.Application.Messages.DTOs;

namespace SolutionOrders.Workers.Application.Abstractions;

/// <summary>
/// Read-only access for workers from MongoDB projections.
/// </summary>
public interface IWorkerReadRepository
{
    Task<IReadOnlyList<WorkerDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<WorkerDto> GetByIdAsync(int idWorker, CancellationToken cancellationToken = default);
}
