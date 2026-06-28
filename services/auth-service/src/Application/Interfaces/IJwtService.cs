using MiniShop.Auth.Domain.Entities;
namespace MiniShop.Auth.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}