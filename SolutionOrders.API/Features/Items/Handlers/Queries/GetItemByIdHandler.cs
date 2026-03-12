using Mapster;
using MediatR;
using SolutionOrders.API.Features.Items.Messages.DTOs;
using SolutionOrders.API.Features.Items.Messages.Queries;
using SolutionOrders.API.Features.Items.Providers;

namespace SolutionOrders.API.Features.Items.Handlers.Queries
{
    public class GetItemByIdHandler(IItemProvider itemProvider)
        : IRequestHandler<GetItemByIdQuery, ItemDto?>
    {
        public async Task<ItemDto?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken) 
            =>
                (await itemProvider.GetItemByIdAsync(request.Id, true ,cancellationToken))?
                .Adapt<ItemDto>();
    }
}