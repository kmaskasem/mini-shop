using Microsoft.EntityFrameworkCore;
using MiniShop.Auth.Domain.Entities;
using MiniShop.Auth.Domain.Interfaces;
using MiniShop.Auth.Infrastructure.Persistence;

namespace MiniShop.Auth.Infrastructure.Persistence.Repositories;

//Postgres
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}