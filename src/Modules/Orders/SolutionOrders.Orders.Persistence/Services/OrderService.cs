using Microsoft.EntityFrameworkCore;
using SolutionOrders.Orders.Application.Services;
using SolutionOrders.Orders.Domain.Entities;

namespace SolutionOrders.Orders.Persistence.Services;

/// <inheritdoc cref="IOrderService" />
public sealed class OrderService(DbContext context) : IOrderService
{
    /// <inheritdoc />
    public async Task CreateOrder(Order order, CancellationToken cancellationToken)
    {
        context.Set<Order>().Add(order);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
