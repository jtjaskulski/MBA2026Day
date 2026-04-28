using MediatR;

namespace SolutionOrders.Workers.Application.Messages.Commands
{
    public class CreateWorkerCommand : IRequest<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Login { get; set; } = string.Empty;
    }
}


