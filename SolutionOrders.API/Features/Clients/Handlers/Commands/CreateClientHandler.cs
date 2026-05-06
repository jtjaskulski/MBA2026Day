using Mapster;
using MediatR;
using SolutionOrders.API.Features.Clients.Messages.Commands;
using SolutionOrders.API.Features.Clients.Services;
using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Clients.Handlers.Commands
{
    public class CreateClientHandler(IClientService clientService, ILogger<CreateClientHandler> logger)
        : IRequestHandler<CreateClientCommand, int>
    {
        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new Client: {Name}", request.Name);
            var client = request.Adapt<Client>();
            await clientService.CreateClient(client, cancellationToken);
            logger.LogInformation("Created client ID: {IdClient}", client.IdClient);
            return client.IdClient;
        }
    }
}
