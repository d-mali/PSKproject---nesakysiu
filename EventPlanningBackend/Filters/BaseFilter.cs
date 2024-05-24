using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class BaseFilter
    {
        public int Page { get; set; } = 1;
        [Range(1, 1000)]
        public int PageSize { get; set; } = 1000;

        public string OrderBy { get; set; } = string.Empty;

        public Sorting? Sort { get; set; }

        public int ItemsToSkip() => (Page - 1) * PageSize;
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        asc,
        desc
    }
}
