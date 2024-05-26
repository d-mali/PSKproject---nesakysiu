namespace EventDomain.Contracts.Responses
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public DateTime? ScheduledTime { get; set; }

        public required string Description { get; set; }
    }
}
