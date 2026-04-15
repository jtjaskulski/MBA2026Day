using Mapster;
using MediatR;
using SolutionOrders.API.Features.Workers.Messages.DTOs;
using SolutionOrders.API.Features.Workers.Messages.Queries;
using SolutionOrders.API.Features.Workers.Providers;

namespace SolutionOrders.API.Features.Workers.Handlers.Queries
{
    public class GetWorkerByIdHandler(IWorkerProvider workerProvider)
        : IRequestHandler<GetWorkerByIdQuery, WorkerDto?>
    {
        public async Task<WorkerDto?> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
            =>
            (await workerProvider.GetWorkerByIdAsync(request.Id, true, cancellationToken))?
            .Adapt<WorkerDto>();
    }
}
