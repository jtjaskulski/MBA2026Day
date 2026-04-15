using MediatR;
using SolutionOrders.API.Features.Workers.Messages.Commands;
using SolutionOrders.API.Features.Workers.Providers;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Workers.Handlers.Commands
{
    public class DeleteWorkerHandler(IWorkerProvider workerProvider, ApplicationDbContext context, ILogger<DeleteWorkerHandler> logger)
        : IRequestHandler<DeleteWorkerCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerProvider.GetWorkerByIdAsync(request.IdWorker, false, cancellationToken);
            logger.LogInformation("Deleting worker ID: {IdWorker}", request.IdWorker);
            worker.IsActive = false;
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Deleted worker ID: {IdWorker}", request.IdWorker);
            return Unit.Value;
        }
    }
}
