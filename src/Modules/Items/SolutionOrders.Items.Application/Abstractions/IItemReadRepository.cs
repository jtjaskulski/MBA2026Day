using SolutionOrders.Items.Application.Messages.DTOs;

namespace SolutionOrders.Items.Application.Abstractions;

/// <summary>
/// Read-only catalog queries backed by MongoDB projections.
/// </summary>
public interface IItemReadRepository
{
    Task<IReadOnlyList<ItemDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ItemDto> GetByIdAsync(int idItem, CancellationToken cancellationToken = default);
}
