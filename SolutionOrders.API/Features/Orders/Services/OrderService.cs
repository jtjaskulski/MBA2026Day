using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Orders.Services
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        public async Task CreateOrder(Order order, CancellationToken cancellationToken)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
