using Microsoft.EntityFrameworkCore;
using MiniShop.Auth.Domain.Entities;
using MiniShop.Auth.Domain.Interfaces;
using MiniShop.Auth.Infrastructure.Persistence;

namespace MiniShop.Auth.Infrastructure.Persistence.Repositories;

//Postgres
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context) => _context = context;

    public async Task<User?> GetByUsername(string username) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted);

    public async Task<User?> GetById(Guid id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

    public async Task<List<User>> GetAll() =>
        await _context.Users.Where(u => !u.IsDeleted).ToListAsync();

    public async Task Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User user)  // soft delete
    {
        _context.Users.Update(user);     // update IsDeleted = true
        await _context.SaveChangesAsync();
    }

}