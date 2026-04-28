using MediatR;
using SolutionOrders.Clients.Application.Messages.DTOs;

namespace SolutionOrders.Clients.Application.Messages.Queries
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
    }
}



