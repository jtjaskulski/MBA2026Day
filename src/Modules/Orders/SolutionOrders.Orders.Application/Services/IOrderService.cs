using SolutionOrders.Orders.Domain.Entities;

namespace SolutionOrders.Orders.Application.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order, CancellationToken cancellationToken);
    }
}

