using BookingService.Dal;
using BookingService.Dal.Repositories;
using BookingService.Dal.Interfaces;
using BookingService.Logic.Interfaces;
using BookingService.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ExampleCore.HttpLogic;
using ExampleCore.TraceIdLogic;
using ProfileConnectionLib;
using Microsoft.Extensions.Configuration;
using MassTransit;
using BookingService.Sagas;
using Logic.Messaging.Consumers;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// MassTransit
// ----------------------
builder.Services.AddMassTransit(x =>
{
    // Consumer для choreography demo
    x.AddConsumer<PaymentConsumer>();

    // Saga state machine — используем InMemoryRepository
    x.AddSagaStateMachine<BookingStateMachine, BookingState>()
        .InMemoryRepository();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

// ----------------------
// EF Core InMemory database
// ----------------------
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseInMemoryDatabase("BookingDb"));

// ----------------------
// Dependency Injection
// ----------------------
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingServiceImpl>();

// ----------------------
// HttpClient & ExampleCore services
// ----------------------
builder.Services.AddHttpClient(); // регистрация IHttpClientFactory
builder.Services.AddScoped<ITraceIdAccessor, TraceIdAccessor>(); // регистрация TraceIdAccessor
builder.Services.AddScoped<IHttpRequestService, HttpRequestService>();

builder.Services.AddScoped<IProfileServiceClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var http = sp.GetRequiredService<IHttpRequestService>();
    return new ProfileServiceClient(http, config);
});

// ----------------------
// Controllers & Swagger
// ----------------------
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Booking API",
        Version = "v1"
    });
});

var app = builder.Build();

// ----------------------
// Configure HTTP pipeline
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
