using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProfilesService.Infrastructure;
using ProfilesService.Infrastructure.Persistence;
using ProfilesService.Application;
using ProfilesService.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Profiles Service API",
        Version = "v1"
    });
});

// Add DbContext (PostgreSQL)
builder.Services.AddDbContext<ProfilesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MediatR (Application assembly)
builder.Services.AddMediatR(typeof(RegisterUserHandler).Assembly);

// Redis + Semaphore
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddInfrastructureDependencies();

var app = builder.Build();

// Apply migrations automatically (optional, for dev)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProfilesDbContext>();
    db.Database.Migrate();
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profiles Service API v1");
    });
}

app.MapControllers();
app.Run();