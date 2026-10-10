using MediatR;
using SolutionOrders.Clients.Application.Messages.DTOs;

namespace SolutionOrders.Clients.Application.Messages.Queries
{
    public class GetClientByIdQuery(int id) : IRequest<ClientDto?>
    {
        public int Id { get; set; } = id;
    }
}



