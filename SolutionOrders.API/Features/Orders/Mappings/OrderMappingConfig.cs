using Mapster;
using SolutionOrders.API.Features.Orders.Messages.Commands;
using SolutionOrders.API.Features.Orders.Messages.DTOs;
using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Orders.Mappings
{
    public class OrderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, OrderDto>()
                .Map(dest => dest.ClientName, src => src.Client != null ? src.Client.Name : null)
                .Map(dest => dest.WorkerName, src => src.Worker != null
                    ? src.Worker.FirstName + " " + src.Worker.LastName : null)
                .Map(dest => dest.OrderItems, src => src.OrderItems);

            config.NewConfig<OrderItem, OrderItemDto>()
                .Map(dest => dest.ItemName, src => src.Item != null ? src.Item.Name : null)
                .Map(dest => dest.Price, src => src.Item != null ? src.Item.Price : null);

            config.NewConfig<CreateOrderItemDto, OrderItem>()
                .Map(dest => dest.IsActive, _ => true)
                .Ignore(dest => dest.IdOrderItem)
                .Ignore(dest => dest.IdOrder)
                .Ignore(dest => dest.Order)
                .Ignore(dest => dest.Item);
        }
    }
}