using Mapster;
using MediatR;
using SolutionOrders.API.Features.Clients.Messages.DTOs;
using SolutionOrders.API.Features.Clients.Messages.Queries;
using SolutionOrders.API.Features.Clients.Providers;

namespace SolutionOrders.API.Features.Clients.Handlers.Queries
{
    public class GetClientByIdHandler(IClientProvider clientProvider)
        : IRequestHandler<GetClientByIdQuery, ClientDto?>
    {
        public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
            =>
            (await clientProvider.GetClientByIdAsync(request.Id, true, cancellationToken))?
            .Adapt<ClientDto>();
    }
}
