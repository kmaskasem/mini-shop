using MiniShop.Auth.Application.Common;
using MiniShop.Auth.Application.DTOs;
using MiniShop.Auth.Application.Interfaces;
using MiniShop.Auth.Domain.Entities;
using MiniShop.Auth.Domain.Enums;
using MiniShop.Auth.Domain.Interfaces;

namespace MiniShop.Auth.Application.UseCases.Users;

public class CreateUserUseCase(IUserRepository userRepo, IPasswordHasher hasher)
{
    public async Task<Result<UserResponse>> Execute(CreateUserRequest request, UserRole requesterRole)
    {
        // Admin สร้างได้แค่ Admin, Staff — ไม่สร้าง Master
        if (request.Role == UserRole.Master)
            return Result<UserResponse>.Failure("Cannot create Master user");

        if (requesterRole == UserRole.Admin && request.Role == UserRole.Admin)
        {
            // Admin สร้าง Admin ได้ — ผ่าน
        }
        else if (requesterRole == UserRole.Admin)
        {
            // Admin สร้างได้แค่ Staff
            if (request.Role != UserRole.Staff)
                return Result<UserResponse>.Failure("Admin can only create Staff or Admin");
        }

        var existing = await userRepo.GetByUsername(request.Username);
        if (existing != null)
            return Result<UserResponse>.Failure("Username already exists");

        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = hasher.Hash(request.Password),
            Role = request.Role
        };

        await userRepo.Create(user);
        return Result<UserResponse>.Success(new UserResponse(user.Id, user.Username, user.Role));
    }
}