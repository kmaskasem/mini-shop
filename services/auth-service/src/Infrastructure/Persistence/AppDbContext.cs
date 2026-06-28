using Microsoft.EntityFrameworkCore;
using MiniShop.Auth.Domain.Entities;

namespace MiniShop.Auth.Infrastructure.Persistence;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // ตรงนี้เอาไว้ Config ว่าตาราง User จะมีหน้าตาเป็นยังไง
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        // modelBuilder.Entity<User>()
        // .Property(u => u.Role)
        // .HasConversion<string>();
    }
}