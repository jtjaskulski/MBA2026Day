using MediatR;
using SolutionOrders.Workers.Application.Messages.Commands;
using SolutionOrders.Workers.Application.Abstractions;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Workers.Application.Handlers.Commands
{
    public class DeleteWorkerHandler(IWorkerProvider workerProvider, IUnitOfWork unitOfWork, ILogger<DeleteWorkerHandler> logger)
        : IRequestHandler<DeleteWorkerCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerProvider.GetWorkerByIdAsync(request.IdWorker, false, cancellationToken);
            logger.LogInformation("Deleting worker ID: {IdWorker}", request.IdWorker);
            worker.IsActive = false;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Deleted worker ID: {IdWorker}", request.IdWorker);
            return Unit.Value;
        }
    }
}

