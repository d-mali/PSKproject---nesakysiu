using System.Diagnostics;
using System.Security.Claims;

namespace EventBackend.Middleware
{
    public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var user = context.User;
            var userId = user.Identity != null && user.Identity.IsAuthenticated ? "anonymous" : user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.Identity != null && user.Identity.IsAuthenticated ? user.Identity.Name : "anonymous";
            var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            logger.LogInformation("Handling request: {Method} {Path} by User: {UserId}, {UserName} Roles: {UserRoles} Date:{Date}", context.Request.Method, context.Request.Path, userId, userName, string.Join(",", userRoles), DateTime.Now);
            await next(context);

            stopwatch.Stop();
            logger.LogInformation("Finished handling request. Duration: {Duration}ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
