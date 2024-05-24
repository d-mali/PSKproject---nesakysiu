
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventBackend.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var user = context.User;  
            var userId = user.Identity != null && user.Identity.IsAuthenticated ? "anonymous" : user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = user.Identity != null && user.Identity.IsAuthenticated ? user.Identity.Name : "anonymous";
            var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            _logger.LogInformation("Handling request: {Method} {Path} by User: {UserId}, {UserName} Roles: {UserRoles} Date:{Date}", context.Request.Method, context.Request.Path, userId, userName, string.Join(",", userRoles), DateTime.Now);
            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation("Finished handling request. Duration: {Duration}ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
