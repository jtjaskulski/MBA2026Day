using MediatR;
using SolutionOrders.Clients.Application.Messages.DTOs;
using SolutionOrders.Clients.Application.Messages.Queries;
using SolutionOrders.Clients.Application.Abstractions;

namespace SolutionOrders.Clients.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetClientByIdQuery"/> via MongoDB projections.</summary>
public sealed class GetClientByIdHandler(IClientReadRepository readRepository)
    : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    /// <inheritdoc />
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await readRepository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
