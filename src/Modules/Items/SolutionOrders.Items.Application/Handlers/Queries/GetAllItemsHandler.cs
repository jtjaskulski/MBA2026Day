using MediatR;
using SolutionOrders.Items.Application.Messages.DTOs;
using SolutionOrders.Items.Application.Messages.Queries;
using SolutionOrders.Items.Application.Abstractions;

namespace SolutionOrders.Items.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetAllItemsQuery"/> via MongoDB projections.</summary>
public sealed class GetAllItemsHandler(IItemReadRepository readRepository)
    : IRequestHandler<GetAllItemsQuery, IEnumerable<ItemDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        => await readRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
}
