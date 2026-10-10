using MediatR;
using Microsoft.EntityFrameworkCore;
using SolutionOrders.Orders.Application.Messages.Commands;
using SolutionOrders.Orders.Application.Abstractions;
using SolutionOrders.Orders.Domain.Entities;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Orders.Application.Handlers.Commands;

/// <summary>
/// Hard-deletes an order aggregate (cascade removes lines via EF configuration).
/// </summary>
public class DeleteOrderHandler(IOrderProvider orderProvider, DbContext db, IUnitOfWork unitOfWork, ILogger<DeleteOrderHandler> logger)
    : IRequestHandler<DeleteOrderCommand, Unit>
{
    /// <inheritdoc />
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderProvider.GetOrderByIdAsync(request.IdOrder, false, cancellationToken).ConfigureAwait(false);
        logger.LogInformation("Deleting order ID: {IdOrder}", request.IdOrder);

        db.Set<Order>().Remove(order);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        logger.LogInformation("Deleted order ID: {IdOrder}", request.IdOrder);
        return Unit.Value;
    }
}
