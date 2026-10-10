using MediatR;
using SolutionOrders.Clients.Application.Messages.Commands;
using SolutionOrders.Clients.Application.Abstractions;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Clients.Application.Handlers.Commands
{
    public class DeleteClientHandler(IClientProvider clientProvider, IUnitOfWork unitOfWork, ILogger<DeleteClientHandler> logger)
        : IRequestHandler<DeleteClientCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await clientProvider.GetClientByIdAsync(request.IdClient, false, cancellationToken);
            logger.LogInformation("Deleting client ID: {IdClient}", request.IdClient);
            client.IsActive = false;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Deleted client ID: {IdClient}", request.IdClient);
            return Unit.Value;
        }
    }
}



