using Mapster;
using MediatR;
using SolutionOrders.Workers.Application.Messages.Commands;
using SolutionOrders.Workers.Application.Services;
using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Workers.Application.Handlers.Commands
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

