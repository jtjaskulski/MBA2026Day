using MediatR;
using SolutionOrders.Items.Application.Messages.DTOs;
using SolutionOrders.Items.Application.Messages.Queries;
using SolutionOrders.Items.Application.Abstractions;

namespace SolutionOrders.Items.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetItemByIdQuery"/> via MongoDB projections.</summary>
public sealed class GetItemByIdHandler(IItemReadRepository readRepository)
    : IRequestHandler<GetItemByIdQuery, ItemDto?>
{
    /// <inheritdoc />
    public async Task<ItemDto?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
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
