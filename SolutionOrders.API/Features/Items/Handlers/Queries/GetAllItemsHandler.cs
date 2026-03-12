using Mapster;
using MediatR;
using SolutionOrders.API.Features.Items.Messages.DTOs;
using SolutionOrders.API.Features.Items.Messages.Queries;
using SolutionOrders.API.Features.Items.Providers;

namespace SolutionOrders.API.Features.Items.Handlers.Queries
{
    public class GetAllItemsHandler(IItemProvider itemProvider)
        : IRequestHandler<GetAllItemsQuery, IEnumerable<ItemDto>>
    {
        public async Task<IEnumerable<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken) 
            =>
            (await itemProvider.GetAllItemsAsync(true ,cancellationToken))
            .Adapt<IEnumerable<ItemDto>>();
    }
}