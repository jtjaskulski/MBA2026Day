using Mapster;
using MediatR;
using SolutionOrders.API.Features.Workers.Messages.DTOs;
using SolutionOrders.API.Features.Workers.Messages.Queries;
using SolutionOrders.API.Features.Workers.Providers;

namespace SolutionOrders.API.Features.Workers.Handlers.Queries
{
    public class GetAllWorkersHandler(IWorkerProvider workerProvider)
        : IRequestHandler<GetAllWorkersQuery, IEnumerable<WorkerDto>>
    {
        public async Task<IEnumerable<WorkerDto>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
            =>
            (await workerProvider.GetAllWorkersAsync(true, cancellationToken))
            .Adapt<IEnumerable<WorkerDto>>();
    }
}
