using MediatR;
using SolutionOrders.Clients.Application.Messages.DTOs;
using SolutionOrders.Clients.Application.Messages.Queries;
using SolutionOrders.Clients.Application.Abstractions;

namespace SolutionOrders.Clients.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetAllClientsQuery"/> via MongoDB projections.</summary>
public sealed class GetAllClientsHandler(IClientReadRepository readRepository)
    : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        => await readRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
}
