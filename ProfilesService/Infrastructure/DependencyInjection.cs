using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ProfilesService.Application.Interfaces;
using ProfilesService.Application.Services;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Infrastructure.Repositories;
using Application.Common.Synchronization;
using Infrastructure.Redis;
using StackExchange.Redis;

namespace ProfilesService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Services
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Redis connection
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

        // Distributed semaphore
        services.AddSingleton<IDistributedSemaphore, RedisDistributedSemaphore>();

        return services;
    }
}