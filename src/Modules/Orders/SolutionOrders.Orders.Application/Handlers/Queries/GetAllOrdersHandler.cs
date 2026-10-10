using MediatR;
using SolutionOrders.Orders.Application.Messages.DTOs;
using SolutionOrders.Orders.Application.Messages.Queries;
using SolutionOrders.Orders.Application.Abstractions;

namespace SolutionOrders.Orders.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetAllOrdersQuery"/> via MongoDB projections.</summary>
public sealed class GetAllOrdersHandler(IOrderReadRepository readRepository)
    : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        => await readRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
}
