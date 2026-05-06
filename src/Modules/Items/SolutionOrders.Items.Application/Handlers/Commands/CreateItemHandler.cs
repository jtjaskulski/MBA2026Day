using Mapster;
using MediatR;
using SolutionOrders.Items.Application.Messages.Commands;
using SolutionOrders.Items.Application.Services;
using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Items.Application.Handlers.Commands
{
    public class CreateItemHandler(IItemService itemService, ILogger<CreateItemHandler> logger)
        : IRequestHandler<CreateItemCommand, int>
    {
        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new Item: {Name}", request.Name);
            var item = request.Adapt<Item>();
            await itemService.CreateItem(item, cancellationToken);
            logger.LogInformation("Created item ID: {IdItem}", item.IdItem);
            return item.IdItem;
        }
    }
}

