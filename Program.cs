using Tasks.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("Environment: Development");
}
else
{
    Console.WriteLine("Environment: Production");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

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
