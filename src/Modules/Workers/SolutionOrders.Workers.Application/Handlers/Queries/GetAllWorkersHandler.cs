using MediatR;
using SolutionOrders.Workers.Application.Messages.DTOs;
using SolutionOrders.Workers.Application.Messages.Queries;
using SolutionOrders.Workers.Application.Abstractions;

namespace SolutionOrders.Workers.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetAllWorkersQuery"/> via MongoDB projections.</summary>
public sealed class GetAllWorkersHandler(IWorkerReadRepository readRepository)
    : IRequestHandler<GetAllWorkersQuery, IEnumerable<WorkerDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<WorkerDto>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
        => await readRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
}
