using MiniShop.Auth.Application.Interfaces;
using MiniShop.Auth.Domain.Interfaces;
using MiniShop.Auth.Application.Common;    // สำหรับ Result
using MiniShop.Auth.Application.DTOs;      // สำหรับ LoginRequest

namespace MiniShop.Auth.Application.UseCases;

public class LoginUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtService _jwtService;

    public LoginUseCase(IUserRepository userRepo, IPasswordHasher hasher, IJwtService jwtService)
    {
        _userRepo = userRepo;
        _hasher = hasher;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginResponse>> Execute(LoginRequest request)
    {
        var user = await _userRepo.GetByUsername(request.Username);
        if (user == null || !_hasher.Verify(user.PasswordHash, request.Password))
        {
            return Result<LoginResponse>.Failure("Invalid credentials");
        }

        var token = _jwtService.GenerateToken(user);
        return Result<LoginResponse>.Success(
            new LoginResponse(token, user.Username));
    }
}