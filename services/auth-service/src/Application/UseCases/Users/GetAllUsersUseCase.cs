using MiniShop.Auth.Application.Common;
using MiniShop.Auth.Application.DTOs;
using MiniShop.Auth.Domain.Interfaces;

namespace MiniShop.Auth.Application.UseCases.Users;
public class GetAllUsersUseCase(IUserRepository userRepo)
{
    // private readonly IUserRepository _userRepository;

    // public GetAllUsersUseCase(IUserRepository userRepository)
    // {
    //     _userRepository = userRepository;
    // }

    public async Task<Result<List<UserResponse>>> Execute()
    {
        var users = await userRepo.GetAll();
        var response = users.Select(
            u => new UserResponse(u.Id, u.Username, u.Role))
            .ToList();
        return Result<List<UserResponse>>.Success(response);
    }
}