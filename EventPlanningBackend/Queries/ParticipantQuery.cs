using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class ParticipantQuery : BaseQuery<ParticipantOrderBy>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? Email { get; set; }
    }


    [DefaultValue(FirstName)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ParticipantOrderBy
    {
        FirstName,
        LastName,
        BirthDate,
        Email,
        Id
    }
}
