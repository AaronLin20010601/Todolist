using Microsoft.EntityFrameworkCore;
using Todolist_Backend.Models;
using Todolist_Backend.Settings;
using Todolist_Backend.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 加入 DbContext 並使用 PostgreSQL 連線
builder.Services.AddDbContext<TodolistDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MailjetSettings>(builder.Configuration.GetSection("Mailjet"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<TokenService>();

// 加入 JWT 驗證
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

// 設置 CORS，允許跨域請求
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// 其他服務註冊
builder.Services.AddControllers();

var app = builder.Build();

// 設置跨域中間件
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5000");
