using Microsoft.AspNetCore.Authorization;

namespace EventBackend.Authorization
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var identity = context.User.Identity;

            if (identity == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //check if authenticated
            if (!identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //complete
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
