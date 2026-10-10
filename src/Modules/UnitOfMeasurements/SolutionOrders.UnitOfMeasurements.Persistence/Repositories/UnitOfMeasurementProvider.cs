using Microsoft.EntityFrameworkCore;
using SolutionOrders.UnitOfMeasurements.Application.Abstractions;
using SolutionOrders.UnitOfMeasurements.Domain.Entities;

namespace SolutionOrders.UnitOfMeasurements.Persistence.Repositories;

/// <inheritdoc cref="IUnitOfMeasurementProvider" />
public sealed class UnitOfMeasurementProvider(DbContext context) : IUnitOfMeasurementProvider
{
    /// <inheritdoc />
    public async Task<IEnumerable<UnitOfMeasurement>> GetAllUnitOfMeasurementsAsync(bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<UnitOfMeasurement>().Where(u => u.IsActive);

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.OrderBy(u => u.Name).ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
