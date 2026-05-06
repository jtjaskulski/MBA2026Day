namespace SolutionOrders.API.Features.Clients.Messages.DTOs
{
    public class ClientDto
    {
        public int IdClient { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
