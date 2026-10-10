using MediatR;
using SolutionOrders.Orders.Application.Messages.DTOs;
using SolutionOrders.Orders.Application.Messages.Queries;
using SolutionOrders.Orders.Application.Abstractions;

namespace SolutionOrders.Orders.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetOrderByIdQuery"/> via MongoDB projections.</summary>
public sealed class GetOrderByIdHandler(IOrderReadRepository readRepository)
    : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    /// <inheritdoc />
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
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
