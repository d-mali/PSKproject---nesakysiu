using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
