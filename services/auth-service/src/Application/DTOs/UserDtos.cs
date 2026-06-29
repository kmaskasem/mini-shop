using MiniShop.Auth.Domain.Enums;

namespace MiniShop.Auth.Application.DTOs;

public record CreateUserRequest(string Username, string Password, UserRole Role);
public record UpdateUserRoleRequest(UserRole NewRole);
public record UserResponse(Guid Id, string Username, UserRole Role);