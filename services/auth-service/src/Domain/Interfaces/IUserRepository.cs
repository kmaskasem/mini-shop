using MiniShop.Auth.Domain.Entities;
namespace MiniShop.Auth.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsername(string username);
    Task<User?> GetById(Guid id);
    Task<List<User>> GetAll();
    Task Create(User user);
    Task Update(User user);
    Task Delete(User user);
}

