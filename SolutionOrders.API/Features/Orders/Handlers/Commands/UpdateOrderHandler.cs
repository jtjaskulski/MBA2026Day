using Mapster;
using MediatR;
using SolutionOrders.API.Features.Orders.Messages.Commands;
using SolutionOrders.API.Features.Orders.Messages.DTOs;
using SolutionOrders.API.Features.Orders.Providers;
using SolutionOrders.API.Models;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Orders.Handlers.Commands
{
    public class UpdateOrderHandler(IOrderProvider orderProvider, ApplicationDbContext context, ILogger<UpdateOrderHandler> logger)
        : IRequestHandler<UpdateOrderCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderProvider.GetOrderByIdAsync(request.IdOrder, false, cancellationToken);
            logger.LogInformation("Updating order ID: {IdOrder}", request.IdOrder);

            order.IdClient = request.IdClient;
            order.IdWorker = request.IdWorker;
            order.Notes = request.Notes;
            order.DeliveryDate = request.DeliveryDate;

            context.OrderItems.RemoveRange(order.OrderItems);
            order.OrderItems = request.OrderItems.Adapt<List<OrderItem>>();

            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Updated order ID: {IdOrder}", request.IdOrder);
            return Unit.Value;
        }
    }
}
