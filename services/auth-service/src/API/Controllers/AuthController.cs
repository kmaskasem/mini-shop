using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Auth.Application.UseCases;
using MiniShop.Auth.Application.DTOs;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly LoginUseCase _loginUseCase;

    public AuthController(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }
    // private readonly AppDbContext _context;
    // public AuthController(AppDbContext context)
    // {
    //     _context = context;
    // }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // 1. เรียกใช้ UseCase
        var result = await _loginUseCase.Execute(request);

        // 2. ถ้าสำเร็จ ส่ง 200 OK พร้อมข้อมูล
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        // 3. ถ้าพลาด ส่ง 400 Bad Request พร้อมข้อความ Error
        return BadRequest(new { message = result.Error });
    }

}