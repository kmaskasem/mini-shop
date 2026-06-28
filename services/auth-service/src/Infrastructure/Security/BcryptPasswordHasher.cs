using MiniShop.Auth.Application.Interfaces;
using BCrypt.Net;

namespace MiniShop.Auth.Infrastructure.Security;
public class BcryptPasswordHasher : IPasswordHasher {
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string hashedPassword, string password) 
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}