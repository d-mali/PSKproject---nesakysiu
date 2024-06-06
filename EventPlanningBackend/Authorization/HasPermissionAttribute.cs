using Microsoft.AspNetCore.Authorization;

namespace EventBackend.Authorization
{
    public class HasPermissionAttribute(string permission) : AuthorizeAttribute(policy: permission);
}
