using MiniShop.Auth.Application.Interfaces;
using MiniShop.Auth.Application.UseCases;
using MiniShop.Auth.Domain.Interfaces;
using MiniShop.Auth.Infrastructure.Persistence.Repositories;
using MiniShop.Auth.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
namespace MiniShop.Auth.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<LoginUseCase>();
        services.AddScoped<IUserRepository, UserRepository>();
        // ผูก Interface กับ Implementations
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}