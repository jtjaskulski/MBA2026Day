using Microsoft.EntityFrameworkCore;
using SolutionOrders.Orders.Application.Abstractions;
using SolutionOrders.Orders.Domain.Entities;

namespace SolutionOrders.Orders.Persistence.Repositories;

/// <inheritdoc cref="IOrderProvider" />
public sealed class OrderProvider(DbContext context) : IOrderProvider
{
    /// <inheritdoc />
    public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Order>()
            .Include(o => o.Client)
            .Include(o => o.Worker)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Item);

        if (asNoTracking)
            return await query.AsNoTracking().OrderByDescending(o => o.DataOrder).ToListAsync(cancellationToken)
                .ConfigureAwait(false);

        return await query.OrderByDescending(o => o.DataOrder).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Order> GetOrderByIdAsync(int id, bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Order>()
            .Include(o => o.Client)
            .Include(o => o.Worker)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Item);

        Order? order;
        if (asNoTracking)
            order = await query.AsNoTracking().FirstOrDefaultAsync(o => o.IdOrder == id, cancellationToken)
                .ConfigureAwait(false);
        else
            order = await query.FirstOrDefaultAsync(o => o.IdOrder == id, cancellationToken).ConfigureAwait(false);

        return order ?? throw new KeyNotFoundException($"Order with ID {id} not found");
    }
}
