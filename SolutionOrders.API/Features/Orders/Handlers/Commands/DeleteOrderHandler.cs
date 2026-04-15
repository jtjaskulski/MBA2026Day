using MediatR;
using SolutionOrders.API.Features.Orders.Messages.Commands;
using SolutionOrders.API.Features.Orders.Providers;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Orders.Handlers.Commands
{
    public class DeleteOrderHandler(IOrderProvider orderProvider, ApplicationDbContext context, ILogger<DeleteOrderHandler> logger)
        : IRequestHandler<DeleteOrderCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderProvider.GetOrderByIdAsync(request.IdOrder, false, cancellationToken);
            logger.LogInformation("Deleting order ID: {IdOrder}", request.IdOrder);

            context.Orders.Remove(order);
            await context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Deleted order ID: {IdOrder}", request.IdOrder);
            return Unit.Value;
        }
    }
}
