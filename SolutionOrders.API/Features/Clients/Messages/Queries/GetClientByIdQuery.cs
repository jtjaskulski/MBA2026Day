using MediatR;
using SolutionOrders.API.Features.Clients.Messages.DTOs;

namespace SolutionOrders.API.Features.Clients.Messages.Queries
{
    public class GetClientByIdQuery(int id) : IRequest<ClientDto?>
    {
        public int Id { get; set; } = id;
    }
}
