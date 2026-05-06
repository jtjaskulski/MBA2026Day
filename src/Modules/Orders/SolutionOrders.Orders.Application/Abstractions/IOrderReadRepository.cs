using SolutionOrders.Orders.Application.Messages.DTOs;

namespace SolutionOrders.Orders.Application.Abstractions;

/// <summary>
/// Read-only order queries backed by MongoDB projections (denormalized for API shape).
/// </summary>
public interface IOrderReadRepository
{
    Task<IReadOnlyList<OrderDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<OrderDto> GetByIdAsync(int idOrder, CancellationToken cancellationToken = default);
}
