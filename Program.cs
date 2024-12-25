using Tasks.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(',')
    ?? Environment.GetEnvironmentVariable("AllowedOrigins")?.Split(',')
    ?? new string[] { "http://localhost:5173" };

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine($"Allowed Origins (Development): {string.Join(", ", allowedOrigins)}");
}
else
{
    Console.WriteLine($"Allowed Origins (Production): {string.Join(", ", allowedOrigins)}");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", corsBuilder =>
    {
        corsBuilder
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseCors("MyCorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();
