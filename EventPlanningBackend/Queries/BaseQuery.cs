using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class BaseQuery<T> where T : Enum
    {
        public int Skip { get; set; } = 0;

        [Range(1, 1000)]
        public int Take { get; set; } = 50;

        public T OrderBy { get; set; } = default!;

        [DefaultValue(nameof(Sorting.Asc))]
        public Sorting Sort { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        Asc,
        Desc
    }
}
