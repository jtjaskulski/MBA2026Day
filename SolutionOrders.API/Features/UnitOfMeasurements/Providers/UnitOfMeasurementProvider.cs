using Microsoft.EntityFrameworkCore;
using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.UnitOfMeasurements.Providers
{
    public class UnitOfMeasurementProvider(ApplicationDbContext context) : IUnitOfMeasurementProvider
    {
        public async Task<IEnumerable<UnitOfMeasurement>> GetAllUnitOfMeasurementsAsync(bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.UnitOfMeasurements
                .Where(u => u.IsActive);

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query
                .OrderBy(u => u.Name)
                .ToListAsync(cancellationToken);
        }
    }
}