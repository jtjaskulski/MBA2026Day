using MediatR;

namespace SolutionOrders.Workers.Application.Messages.Commands
{
    public class UpdateWorkerCommand : IRequest<Unit>
    {
        public int IdWorker { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Login { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}


