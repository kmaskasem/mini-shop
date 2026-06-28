using MiniShop.Auth.Domain.Entities;
using MiniShop.Auth.Domain.Enums;
using MiniShop.Auth.Infrastructure.Persistence;
using MiniShop.Auth.Infrastructure.Security;

namespace MiniShop.Auth.Infrastructure.Services;
public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        var passwordHasher = new BcryptPasswordHasher();
        var admin = context.Users.SingleOrDefault(u => u.Username == "admintt");

        if (admin == null)
        {
            admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admintt",
                Role = UserRole.Admin
            };
            context.Users.Add(admin);
        }

        admin.PasswordHash = passwordHasher.Hash("1234");
        context.SaveChanges();
    }
}