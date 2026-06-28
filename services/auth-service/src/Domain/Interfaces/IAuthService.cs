namespace MiniShop.Auth.Domain.Interfaces;

public interface IAuthService
{
    Task<string> Login(string username, string password); // คืนค่าเป็น Token
    
}