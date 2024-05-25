namespace EventBackend.Filters
{
    public class EventsQuery : BaseQuery<EventOrderBy>
    {
        public string? Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public enum EventOrderBy
    {
        StartDate,
        Id
    }
}
