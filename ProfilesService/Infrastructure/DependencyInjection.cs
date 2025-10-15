using Microsoft.Extensions.DependencyInjection;
using ProfilesService.Application.Interfaces;
using ProfilesService.Application.Services;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Infrastructure.Repositories;

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
}