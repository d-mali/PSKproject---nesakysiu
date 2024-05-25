using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class BaseQuery<T> where T : Enum
    {
        public int Page { get; set; } = 1;
        [Range(1, 1000)]
        public int PageSize { get; set; } = 1000;

        public T? OrderBy { get; set; }

        [DefaultValue(Sorting.Asc)]
        public Sorting? Sort { get; set; }

        public int ItemsToSkip() => (Page - 1) * PageSize;
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        Asc,
        Desc
    }
}
