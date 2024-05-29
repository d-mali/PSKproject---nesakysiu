namespace EventDomain.Contracts.Responses
{
    public class EventResponse
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
