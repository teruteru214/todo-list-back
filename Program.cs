using Tasks.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("SQLCONNSTR_DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("接続文字列が取得できません。環境変数を確認してください。");
    throw new InvalidOperationException("Database connection string is not configured.");
}
else
{
    Console.WriteLine($"接続文字列: {connectionString}");
}

try
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
catch (Exception ex)
{
    Console.WriteLine($"DB接続に失敗: {ex.Message}");
    throw;
}

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
