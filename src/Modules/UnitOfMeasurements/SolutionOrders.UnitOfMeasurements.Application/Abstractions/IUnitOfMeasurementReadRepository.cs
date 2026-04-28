using SolutionOrders.UnitOfMeasurements.Application.Messages.DTOs;

namespace SolutionOrders.UnitOfMeasurements.Application.Abstractions;

/// <summary>
/// Read-only listing for units of measurement from MongoDB projections.
/// </summary>
public interface IUnitOfMeasurementReadRepository
{
    Task<IReadOnlyList<UnitOfMeasurementDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
