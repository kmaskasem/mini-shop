using MiniShop.Auth.Domain.Entities;
using MiniShop.Auth.Domain.Enums;
using MiniShop.Auth.Infrastructure.Persistence;
using MiniShop.Auth.Infrastructure.Security;
using Microsoft.Extensions.Configuration;

namespace MiniShop.Auth.Infrastructure.Services;

public static class SeedData
{
    public static void Initialize(AppDbContext context, IConfiguration configuration)
    {
        var passwordHasher = new BcryptPasswordHasher();
        var masterUsername = configuration["Seed:MasterUsername"] ?? throw new InvalidOperationException("Seed:MasterUsername is required");
        var masterPassword = configuration["Seed:MasterPassword"] ?? throw new InvalidOperationException("Seed:MasterPassword is required");

        var master = context.Users.SingleOrDefault(u => u.Username == masterUsername);

        if (master == null)
        {
            master = new User
            {
                Id = Guid.NewGuid(),
                Username = masterUsername,
                Role = UserRole.Master
            };
            context.Users.Add(master);
        }

        master.PasswordHash = passwordHasher
        .Hash(masterPassword);
        context.SaveChanges();
    }
}