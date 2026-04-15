namespace SolutionOrders.API.Features.Workers.Messages.DTOs
{
    public class WorkerDto
    {
        public int IdWorker { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Login { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
