using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolutionOrders.Orders.Application.Messages.Commands;
using SolutionOrders.Orders.Application.Messages.DTOs;
using SolutionOrders.Orders.Application.Abstractions;
using SolutionOrders.Orders.Domain.Entities;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Orders.Application.Handlers.Commands;

/// <summary>
/// Updates an existing order header and replaces order lines (SQL source of truth).
/// </summary>
public class UpdateOrderHandler(IOrderProvider orderProvider, DbContext db, IUnitOfWork unitOfWork, ILogger<UpdateOrderHandler> logger)
    : IRequestHandler<UpdateOrderCommand, Unit>
{
    /// <inheritdoc />
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderProvider.GetOrderByIdAsync(request.IdOrder, false, cancellationToken).ConfigureAwait(false);
        logger.LogInformation("Updating order ID: {IdOrder}", request.IdOrder);

        order.IdClient = request.IdClient;
        order.IdWorker = request.IdWorker;
        order.Notes = request.Notes;
        order.DeliveryDate = request.DeliveryDate;

        db.Set<OrderItem>().RemoveRange(order.OrderItems);
        order.OrderItems.Clear();

        foreach (var line in request.OrderItems.Adapt<List<OrderItem>>())
        {
            line.IdOrder = order.IdOrder;
            order.OrderItems.Add(line);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        logger.LogInformation("Updated order ID: {IdOrder}", request.IdOrder);
        return Unit.Value;
    }
}
