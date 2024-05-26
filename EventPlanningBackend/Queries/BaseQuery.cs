using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventBackend.Filters
{
    public class BaseQuery<T> where T : Enum
    {
        [DefaultValue(0)]
        public int? Skip { get; set; }

        [DefaultValue(50)]
        [Range(1, 1000)]
        public int? Take { get; set; }

        public T? OrderBy { get; set; }

        [DefaultValue(Sorting.Asc)]
        public Sorting? Sort { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        Asc,
        Desc
    }
}
