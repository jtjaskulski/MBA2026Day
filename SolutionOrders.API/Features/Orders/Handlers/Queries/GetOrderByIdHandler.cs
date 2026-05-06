using Mapster;
using MediatR;
using SolutionOrders.API.Features.Orders.Messages.DTOs;
using SolutionOrders.API.Features.Orders.Messages.Queries;
using SolutionOrders.API.Features.Orders.Providers;

namespace SolutionOrders.API.Features.Orders.Handlers.Queries
{
    public class GetOrderByIdHandler(IOrderProvider orderProvider)
        : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            =>
            (await orderProvider.GetOrderByIdAsync(request.Id, true, cancellationToken))?
            .Adapt<OrderDto>();
    }
}
