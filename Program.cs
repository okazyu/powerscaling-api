using Microsoft.EntityFrameworkCore;
using PowerScaling.Data;
using PowerScaling.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BatalhaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173",
                "https://localhost:8080",
                "http://localhost:8080",
                "http://localhost:8081",
                "https://localhost:8081",
                "https://char-showdown.vercel.app/"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
} else
{
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()));
}


var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.MapControllers();

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"Connection: {builder.Configuration.GetConnectionString("DefaultConnection")}");

app.Run();