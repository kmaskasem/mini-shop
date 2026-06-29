using MiniShop.Auth.Application.Common;
using MiniShop.Auth.Domain.Enums;
using MiniShop.Auth.Domain.Interfaces;

namespace MiniShop.Auth.Application.UseCases.Users;

public class DeleteUserUseCase(IUserRepository userRepo)
{
    public async Task<Result<bool>> Execute(Guid targetId, UserRole requesterRole)
    {
        var target = await userRepo.GetById(targetId);
        if (target == null || target.IsDeleted)
            return Result<bool>.Failure("User not found");

        // ห้ามลบ Master
        if (target.Role == UserRole.Master)
            return Result<bool>.Failure("Cannot delete Master");

        // ลบ Admin — Master เท่านั้น
        if (target.Role == UserRole.Admin && requesterRole != UserRole.Master)
            return Result<bool>.Failure("Only Master can delete Admin");

        target.IsDeleted = true;
        await userRepo.Delete(target);
        return Result<bool>.Success(true);
    }
}