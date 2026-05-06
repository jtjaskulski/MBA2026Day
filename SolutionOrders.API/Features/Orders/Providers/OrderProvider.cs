using Microsoft.EntityFrameworkCore;
using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Orders.Providers
{
    public class OrderProvider(ApplicationDbContext context) : IOrderProvider
    {
        public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.Orders
                .Include(o => o.Client)
                .Include(o => o.Worker)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item);

            if (asNoTracking)
                return await query.AsNoTracking().OrderByDescending(o => o.DataOrder).ToListAsync(cancellationToken);

            return await query.OrderByDescending(o => o.DataOrder).ToListAsync(cancellationToken);
        }

        public async Task<Order> GetOrderByIdAsync(int id, bool asNoTracking = true,
            CancellationToken cancellationToken = default)
        {
            var query = context.Orders
                .Include(o => o.Client)
                .Include(o => o.Worker)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item);

            Order? order;
            if (asNoTracking)
                order = await query.AsNoTracking().FirstOrDefaultAsync(o => o.IdOrder == id, cancellationToken);
            else
                order = await query.FirstOrDefaultAsync(o => o.IdOrder == id, cancellationToken);

            return order ?? throw new KeyNotFoundException($"Order with ID {id} not found");
        }
    }
}