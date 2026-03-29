using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Ticket.API.Extensions;
using Ticket.Application.Features.Tickets.Queries;
using Ticket.Application.Interfaces;
using Ticket.Domain.Entities;
using Ticket.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register DbContext (EF Core)
builder.Services.AddDbContext<TicketDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ITicketDbContext>(provider => provider.GetService<TicketDbContext>()!);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddControllers();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAvailableTicketsQuery).Assembly));

// Register Rate Limiting
builder.Services.AddRateLimiter(opts =>
{
    opts.AddFixedWindowLimiter("PublicApi", opt =>
    {
        opt.PermitLimit = 10;
        opt.Window = TimeSpan.FromSeconds(1);
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
app.UseLoggingMiddleware();
app.MapControllers();


// Create seed data for test
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TicketDbContext>();
        await context.Database.MigrateAsync();
        await DbInitializer.SeedData(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Có lỗi xảy ra khi seeding dữ liệu.");
    }
}

app.Run();
