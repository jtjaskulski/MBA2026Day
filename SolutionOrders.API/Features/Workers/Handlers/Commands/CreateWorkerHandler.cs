using Mapster;
using MediatR;
using SolutionOrders.API.Features.Workers.Messages.Commands;
using SolutionOrders.API.Features.Workers.Services;
using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Workers.Handlers.Commands
{
    public class CreateWorkerHandler(IWorkerService workerService, ILogger<CreateWorkerHandler> logger)
        : IRequestHandler<CreateWorkerCommand, int>
    {
        public async Task<int> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new Worker: {FirstName} {LastName}", request.FirstName, request.LastName);
            var worker = request.Adapt<Worker>();
            await workerService.CreateWorker(worker, cancellationToken);
            logger.LogInformation("Created worker ID: {IdWorker}", worker.IdWorker);
            return worker.IdWorker;
        }
    }
}