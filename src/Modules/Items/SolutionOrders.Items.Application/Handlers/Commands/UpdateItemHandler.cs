using Mapster;
using MediatR;
using SolutionOrders.Items.Application.Messages.Commands;
using SolutionOrders.Items.Application.Abstractions;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.Items.Application.Handlers.Commands
{
    public class UpdateItemHandler(IItemProvider itemProvider, IUnitOfWork unitOfWork, ILogger<UpdateItemHandler> logger)
        : IRequestHandler<UpdateItemCommand, Unit>
    {
        public async Task<Unit> Handle(
            UpdateItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = await itemProvider.GetItemByIdAsync(request.IdItem, false, cancellationToken);
            logger.LogInformation("Updating item ID: {IdItem}", request.IdItem);
            request.Adapt(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Updated item ID: {IdItem}", request.IdItem);
            return Unit.Value;  // MediatR Unit = void
        }
    }
}

