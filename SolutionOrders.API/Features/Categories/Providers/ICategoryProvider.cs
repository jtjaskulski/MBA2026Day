using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Categories.Providers
{
    public interface ICategoryProvider
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool asNoTracking = true, CancellationToken cancellationToken = default);
    }
}