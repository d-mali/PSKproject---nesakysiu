using System.Text.Json.Serialization;

namespace EventDomain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserPrivilegeType
    {
        Master = 0,
        Admin = 1,
        Worker = 2
    }
}
