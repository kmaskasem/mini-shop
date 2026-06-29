using  MiniShop.Auth.Domain.Enums;

namespace MiniShop.Auth.Domain.Entities;
public class User
{
    public Guid Id { get;set;}
    public string Username { get;set;}
    public string PasswordHash { get;set;}
    public UserRole Role { get;set;} // Master, Admin, Staff
    public bool IsDeleted { get; set; } = false; 
}