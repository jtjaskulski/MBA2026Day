using MediatR;
using SolutionOrders.API.Features.Items.Messages.Commands;
using SolutionOrders.API.Features.Items.Providers;
using SolutionOrders.API.Models.Data;

namespace SolutionOrders.API.Features.Items.Handlers.Commands
{
    public class DeleteItemHandler(IItemProvider itemProvider, 
        ApplicationDbContext context, ILogger<DeleteItemHandler> logger)
        : IRequestHandler<DeleteItemCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = await itemProvider.GetItemByIdAsync(request.IdItem, false, cancellationToken);
            logger.LogInformation("Deleting item ID: {IdItem}", request.IdItem);

            item.IsActive = false;
            await context.SaveChangesAsync(cancellationToken);

            // or hard delete:
            // _context.Items.Remove(item);
            // await _context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Deleted item ID: {IdItem}", request.IdItem);
            return Unit.Value;
        }
    }
}