using MediatR;
using SolutionOrders.Workers.Application.Messages.DTOs;

namespace SolutionOrders.Workers.Application.Messages.Queries
{
    public class GetAllWorkersQuery : IRequest<IEnumerable<WorkerDto>>
    {
    }
}


