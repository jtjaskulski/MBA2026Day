using Mapster;
using MediatR;
using SolutionOrders.API.Features.Orders.Messages.DTOs;
using SolutionOrders.API.Features.Orders.Messages.Queries;
using SolutionOrders.API.Features.Orders.Providers;

namespace SolutionOrders.API.Features.Orders.Handlers.Queries
{
    public class GetAllOrdersHandler(IOrderProvider orderProvider)
        : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            =>
            (await orderProvider.GetAllOrdersAsync(true, cancellationToken))
            .Adapt<IEnumerable<OrderDto>>();
    }
}
