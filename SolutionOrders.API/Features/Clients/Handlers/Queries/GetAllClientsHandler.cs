using Mapster;
using MediatR;
using SolutionOrders.API.Features.Clients.Messages.DTOs;
using SolutionOrders.API.Features.Clients.Messages.Queries;
using SolutionOrders.API.Features.Clients.Providers;

namespace SolutionOrders.API.Features.Clients.Handlers.Queries
{
    public class GetAllClientsHandler(IClientProvider clientProvider)
        : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            =>
            (await clientProvider.GetAllClientsAsync(true, cancellationToken))
            .Adapt<IEnumerable<ClientDto>>();
    }
}
