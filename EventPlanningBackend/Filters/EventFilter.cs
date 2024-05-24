using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class EventFilter : BaseFilter
    {
        public string? Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
