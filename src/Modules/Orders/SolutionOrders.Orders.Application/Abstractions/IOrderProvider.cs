using SolutionOrders.Orders.Domain.Entities;

namespace SolutionOrders.Orders.Application.Abstractions;

/// <summary>
/// SQL-backed order queries for commands that need tracked aggregates (GET list/detail use Mongo projections in a later iteration).
/// </summary>
public interface IOrderProvider
{
    Task<IEnumerable<Order>> GetAllOrdersAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<Order> GetOrderByIdAsync(int id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}

