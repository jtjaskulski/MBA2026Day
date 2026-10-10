using MediatR;
using SolutionOrders.Items.Application.Messages.DTOs;

namespace SolutionOrders.Items.Application.Messages.Queries
{
    public class GetItemByIdQuery(int id) : IRequest<ItemDto?>
    {
        public int Id { get; set; } = id;
    }
}

