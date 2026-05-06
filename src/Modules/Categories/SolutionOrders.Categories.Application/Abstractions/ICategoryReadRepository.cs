using SolutionOrders.Categories.Application.Messages.DTOs;

namespace SolutionOrders.Categories.Application.Abstractions;

/// <summary>
/// Read-only access to categories backed by the MongoDB projection (writes still go to SQL).
/// </summary>
public interface ICategoryReadRepository
{
    /// <summary>
    /// Returns all active categories for listing APIs.
    /// </summary>
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
}

