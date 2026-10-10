using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Items.Application.Abstractions;

/// <summary>
/// Item catalog queries backed by SQL for tracked reads used by commands (Mongo read APIs follow in projections).
/// </summary>
public interface IItemProvider
{
    Task<IEnumerable<Item>> GetAllItemsAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);

    Task<Item> GetItemByIdAsync(int id, bool asNoTracking = true, CancellationToken cancellationToken = default);
}

