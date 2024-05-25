using EventBackend.Middleware;
using EventBackend.Services;
using EventBackend.Services.Interfaces;
using EventDataAccess.Abstractions;
using EventDataAccess.Context;
using EventDataAccess.Repositories;
using EventDomain.Services;
using EventPlanningBackend;
using Microsoft.AspNetCore.Identity;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<IMainDbContext, MainDbContext>();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<MainDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<ITasksService, TasksService>();

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

app.UseMiddleware<OptimisticLockingExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

