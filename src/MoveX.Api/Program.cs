using Microsoft.EntityFrameworkCore;
using MoveX.Application.Bookings;
using MoveX.Application.Operations;
using MoveX.Infrastructure.Data;
using MoveX.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MoveX")
    ?? builder.Configuration["MOVEX_CONNECTION_STRING"];

if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("MoveX database connection string is not configured.");

builder.Services.AddDbContext<MoveXDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IOperationsService, OperationsService>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

var admin = app.MapGroup("/api/admin").WithTags("Admin Operations");

admin.MapGet("/dashboard", async (IOperationsService service, CancellationToken ct) =>
    Results.Ok(await service.GetDashboardAsync(ct)));

admin.MapGet("/bookings", async (int? take, IBookingService service, CancellationToken ct) =>
    Results.Ok(await service.GetRecentAsync(take ?? 50, ct)));

admin.MapGet("/bookings/{id:int}", async (int id, IBookingService service, CancellationToken ct) =>
{
    var booking = await service.GetByIdAsync(id, ct);
    return booking is null ? Results.NotFound() : Results.Ok(booking);
});

admin.MapGet("/bookings/{id:int}/drivers", async (int id, IOperationsService service, CancellationToken ct) =>
    Results.Ok(await service.GetDispatchCandidatesAsync(id, ct)));

app.Run();
