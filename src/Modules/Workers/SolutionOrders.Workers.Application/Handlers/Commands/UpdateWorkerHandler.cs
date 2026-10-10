using Mapster;
using MediatR;
using SolutionOrders.Workers.Application.Messages.Commands;
using SolutionOrders.Workers.Application.Abstractions;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Workers.Application.Handlers.Commands
{
    public class UpdateWorkerHandler(IWorkerProvider workerProvider, IUnitOfWork unitOfWork, ILogger<UpdateWorkerHandler> logger)
        : IRequestHandler<UpdateWorkerCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerProvider.GetWorkerByIdAsync(request.IdWorker, false, cancellationToken);
            logger.LogInformation("Updating worker ID: {IdWorker}", request.IdWorker);
            request.Adapt(worker);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Updated worker ID: {IdWorker}", request.IdWorker);
            return Unit.Value;
        }
    }
}

