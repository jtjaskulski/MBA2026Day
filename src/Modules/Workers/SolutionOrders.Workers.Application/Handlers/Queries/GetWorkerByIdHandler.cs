using MediatR;
using SolutionOrders.Workers.Application.Messages.DTOs;
using SolutionOrders.Workers.Application.Messages.Queries;
using SolutionOrders.Workers.Application.Abstractions;

namespace SolutionOrders.Workers.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetWorkerByIdQuery"/> via MongoDB projections.</summary>
public sealed class GetWorkerByIdHandler(IWorkerReadRepository readRepository)
    : IRequestHandler<GetWorkerByIdQuery, WorkerDto?>
{
    /// <inheritdoc />
    public async Task<WorkerDto?> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await readRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
