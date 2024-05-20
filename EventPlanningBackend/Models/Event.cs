namespace EventPlanningBackend.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string Title { get; set; }
        public string Information { get; set; }

        public List<Task> Tasks { get; } = new();
    }
}
