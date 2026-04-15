using MediatR;

namespace SolutionOrders.API.Features.Clients.Messages.Commands
{
    public class CreateClientCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
