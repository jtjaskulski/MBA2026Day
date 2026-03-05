using MediatR;
using SolutionOrders.API.Features.Items.Messages.DTOs;

namespace SolutionOrders.API.Features.Items.Messages.Queries
{
    public class GetItemByIdQuery(int id) : IRequest<ItemDto?>
    {
        public int Id { get; set; } = id;
    }
}