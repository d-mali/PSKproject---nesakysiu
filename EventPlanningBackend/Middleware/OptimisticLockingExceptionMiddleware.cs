using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

public class OptimisticLockingExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public OptimisticLockingExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 450;

        return context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = "Optimistic Locking"
        }.ToString());
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
