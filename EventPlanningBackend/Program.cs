using EventBackend;
using EventBackend.Authorization;
using EventBackend.Entities;
using EventBackend.Middleware;
using EventBackend.Services;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

RegisterServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
//app.UseMiddleware<OptimisticLockingExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<ApplicationUser>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
return;

static void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(SetupSwaggerGrouping);

    services.AddControllers();
    services.AddDbContext<MainDbContext>();
    services.AddAuthorization();
    services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<MainDbContext>();

    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

    services.AddScoped<IEventsService, EventsService>();
    services.AddScoped<ITasksService, TasksService>();
    services.AddScoped<IParticipantsService, ParticipantsService>();
    services.AddScoped<IUsersService, UsersService>();

    services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
}

static void SetupSwaggerGrouping(SwaggerGenOptions options)
{
    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return [api.GroupName];
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        return [controllerActionDescriptor?.ControllerName];
    });

    options.DocInclusionPredicate((_, api) => true);
}

