using Microsoft.AspNetCore.Authorization;

namespace EventBackend.Authorization
{
    public class PermissionRequirement(string permission) : IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }
}
