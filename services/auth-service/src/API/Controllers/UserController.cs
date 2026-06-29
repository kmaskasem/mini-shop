using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Auth.Application.DTOs;
using MiniShop.Auth.Application.UseCases.Users;
using MiniShop.Auth.Domain.Enums;
using System.Security.Claims;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly GetAllUsersUseCase _getAll;
    private readonly CreateUserUseCase _create;
    private readonly UpdateUserRoleUseCase _updateRole;
    private readonly DeleteUserUseCase _delete;

    public UserController(
        GetAllUsersUseCase getAll,
        CreateUserUseCase create,
        UpdateUserRoleUseCase updateRole,
        DeleteUserUseCase delete)
    {
        _getAll = getAll;
        _create = create;
        _updateRole = updateRole;
        _delete = delete;
    }

    // Get Requester Role
    private UserRole GetRequesterRole() =>
        Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role)!);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.Execute();
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var role = GetRequesterRole();
        if (role == UserRole.Staff)
            return Forbid();

        var result = await _create.Execute(request, role);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
    }

    [HttpPatch("{id}/role")]
    public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateUserRoleRequest request)
    {
        var role = GetRequesterRole();
        if (role == UserRole.Staff)
            return Forbid();

        var result = await _updateRole.Execute(id, request, role);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var role = GetRequesterRole();
        if (role == UserRole.Staff)
            return Forbid();

        var result = await _delete.Execute(id, role);
        return result.IsSuccess ? Ok(new { message = "Deleted" }) : BadRequest(new { message = result.Error });
    }
}