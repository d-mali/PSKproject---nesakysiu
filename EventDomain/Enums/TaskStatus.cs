using System.Text.Json.Serialization;

namespace EventDomain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskStatus
    {
        ToDo = 0,
        InProgress = 1,
        Done = 2
    }
}
