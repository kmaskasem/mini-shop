using MiniShop.Auth.API.Extensions;
using MiniShop.Auth.Infrastructure.Services;
using MiniShop.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// 1. เพิ่มบริการต่างๆ เข้าไป (Dependency Injection)
builder.Services.AddControllers(); // เปลี่ยนมาใช้ Controller แทน Minimal API
builder.Services.AddEndpointsApiExplorer();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
// 2. เชื่อมต่อ DB (ดึง Connection String จาก appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
var app = builder.Build();

// 1. สร้าง Scope เพื่อดึง DbContext ออกมา
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        // 2. สั่ง Migrate ฐานข้อมูลให้เป็นเวอร์ชันล่าสุดโดยอัตโนมัติ
        context.Database.Migrate();

        // 3. เรียกใช้ Seeder ของเรา
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "เกิดข้อผิดพลาดระหว่างการทำ Database Migration/Seeding");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
// บอกให้ระบบไปดู Controller ที่เราสร้างไว้ในโฟลเดอร์ Controllers
app.MapControllers();
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
