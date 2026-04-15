using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Orders.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order, CancellationToken cancellationToken);
    }
}
