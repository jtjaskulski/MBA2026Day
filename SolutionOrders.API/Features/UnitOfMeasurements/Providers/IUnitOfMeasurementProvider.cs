using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.UnitOfMeasurements.Providers
{
    public interface IUnitOfMeasurementProvider
    {
        Task<IEnumerable<UnitOfMeasurement>> GetAllUnitOfMeasurementsAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}