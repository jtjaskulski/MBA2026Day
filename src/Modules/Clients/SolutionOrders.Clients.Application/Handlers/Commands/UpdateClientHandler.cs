using Mapster;
using MediatR;
using SolutionOrders.Clients.Application.Messages.Commands;
using SolutionOrders.Clients.Application.Abstractions;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Clients.Application.Handlers.Commands
{
    public class UpdateClientHandler(IClientProvider clientProvider, IUnitOfWork unitOfWork, ILogger<UpdateClientHandler> logger)
        : IRequestHandler<UpdateClientCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await clientProvider.GetClientByIdAsync(request.IdClient, false, cancellationToken);
            logger.LogInformation("Updating client ID: {IdClient}", request.IdClient);
            request.Adapt(client);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Updated client ID: {IdClient}", request.IdClient);
            return Unit.Value;
        }
    }
}



