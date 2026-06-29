using MiniShop.Auth.Application.Common;
using MiniShop.Auth.Application.DTOs;
using MiniShop.Auth.Domain.Enums;
using MiniShop.Auth.Domain.Interfaces;

namespace MiniShop.Auth.Application.UseCases.Users;

public class UpdateUserRoleUseCase(IUserRepository userRepo)
{
    public async Task<Result<UserResponse>> Execute(Guid targetId, UpdateUserRoleRequest request, UserRole requesterRole)
    {
        var target = await userRepo.GetById(targetId);
        if (target == null || target.IsDeleted)
            return Result<UserResponse>.Failure("User not found");

        // ห้ามแตะ Master
        if (target.Role == UserRole.Master)
            return Result<UserResponse>.Failure("Cannot change Master role");

        // ห้าม set เป็น Master
        if (request.NewRole == UserRole.Master)
            return Result<UserResponse>.Failure("Cannot assign Master role");

        // เฉพาะ Master เท่านั้นที่เปลี่ยน role Admin ได้
        if (target.Role == UserRole.Admin && requesterRole != UserRole.Master)
            return Result<UserResponse>.Failure("Only Master can change Admin role");

        target.Role = request.NewRole;
        await userRepo.Update(target);
        return Result<UserResponse>.Success(new UserResponse(target.Id, target.Username, target.Role));
    }
}