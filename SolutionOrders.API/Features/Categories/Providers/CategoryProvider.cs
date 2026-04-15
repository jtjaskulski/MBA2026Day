using Microsoft.EntityFrameworkCore;
using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Categories.Providers
{
    public class CategoryProvider(ApplicationDbContext context) : ICategoryProvider
    {
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.Categories
                .Where(c => c.IsActive);

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);
        }
    }
}