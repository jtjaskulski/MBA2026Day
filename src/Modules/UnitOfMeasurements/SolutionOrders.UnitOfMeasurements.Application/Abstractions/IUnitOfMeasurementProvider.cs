using SolutionOrders.UnitOfMeasurements.Domain.Entities;

namespace SolutionOrders.UnitOfMeasurements.Application.Abstractions;

/// <summary>
/// Unit-of-measure read queries (SQL-backed until Mongo projections fully replace GET endpoints).
/// </summary>
public interface IUnitOfMeasurementProvider
{
    Task<IEnumerable<UnitOfMeasurement>> GetAllUnitOfMeasurementsAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
}

