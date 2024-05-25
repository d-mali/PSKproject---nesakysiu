using System.ComponentModel;
using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class EventsQuery : BaseQuery<EventOrderBy>
    {
        public string? Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }


    [DefaultValue(EventOrderBy.StartDate)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventOrderBy
    {
        StartDate,
        Id
    }
}
