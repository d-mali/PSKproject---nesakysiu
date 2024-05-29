using System.ComponentModel;
using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class EventsQuery : BaseQuery<EventOrderBy>
    {
        public string? Title { get; set; }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }
    }


    [DefaultValue(StartDate)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EventOrderBy
    {
        StartDate,
        Id
    }
}
