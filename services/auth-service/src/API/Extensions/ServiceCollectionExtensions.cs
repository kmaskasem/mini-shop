using MiniShop.Auth.Application.Interfaces;
using MiniShop.Auth.Application.UseCases;
using MiniShop.Auth.Application.UseCases.Users;
using MiniShop.Auth.Domain.Interfaces;
using MiniShop.Auth.Infrastructure.Persistence.Repositories;
using MiniShop.Auth.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace MiniShop.Auth.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // ผูก Interface กับ Implementations
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();

        // Auth Use Cases
        services.AddScoped<LoginUseCase>();
        services.AddScoped<IUserRepository, UserRepository>();

        // User Use Cases
        services.AddScoped<GetAllUsersUseCase>();
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<UpdateUserRoleUseCase>();
        services.AddScoped<DeleteUserUseCase>();

        // เพิ่ม JWT Authentication
        var jwtSecret = configuration["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt:Secret is required");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

        services.AddAuthorization();
        return services;
    }
}