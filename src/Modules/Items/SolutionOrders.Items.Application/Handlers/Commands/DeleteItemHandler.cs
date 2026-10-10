using MediatR;
using SolutionOrders.Items.Application.Messages.Commands;
using SolutionOrders.Items.Application.Abstractions;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Items.Application.Handlers.Commands
{
    public class DeleteItemHandler(IItemProvider itemProvider, 
        IUnitOfWork unitOfWork, ILogger<DeleteItemHandler> logger)
        : IRequestHandler<DeleteItemCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = await itemProvider.GetItemByIdAsync(request.IdItem, false, cancellationToken);
            logger.LogInformation("Deleting item ID: {IdItem}", request.IdItem);

            item.IsActive = false;
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // or hard delete:
            // _context.Items.Remove(item);
            // await _context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Deleted item ID: {IdItem}", request.IdItem);
            return Unit.Value;
        }
    }
}

