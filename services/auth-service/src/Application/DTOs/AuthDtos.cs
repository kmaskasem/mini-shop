namespace MiniShop.Auth.Application.DTOs;

// ใช้สำหรับรับค่าตอน Login
public record LoginRequest(string Username, string Password);

// ใช้สำหรับส่งค่าผลลัพธ์กลับ
public record LoginResponse(string Token, string Username);