using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Todolist_Backend.Models;
using Todolist_Backend.Settings;

using Todolist_Backend.Services.Interfaces.Email;
using Todolist_Backend.Services.Interfaces.Token;
using Todolist_Backend.Services.Interfaces.VerifyCode;
using Todolist_Backend.Services.Interfaces.Login;
using Todolist_Backend.Services.Interfaces.Register;
using Todolist_Backend.Services.Interfaces.Reset;
using Todolist_Backend.Services.Interfaces.Account;
using Todolist_Backend.Services.Interfaces.Todo;

using Todolist_Backend.Services.Email;
using Todolist_Backend.Services.Token;
using Todolist_Backend.Services.VerifyCode;
using Todolist_Backend.Services.Login;
using Todolist_Backend.Services.Register;
using Todolist_Backend.Services.Reset;
using Todolist_Backend.Services.Account;
using Todolist_Backend.Services.Todo;

var builder = WebApplication.CreateBuilder(args);

// �[�J DbContext �èϥ� PostgreSQL �s�u
builder.Services.AddDbContext<TodolistDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ���U Settings
builder.Services.Configure<MailjetSettings>(builder.Configuration.GetSection("Mailjet"));

// ���U DI
// Email Service
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmailLogger, EmailLogger>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Token Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Verify Code service
builder.Services.AddScoped<IVerificationCode, VerificationCode>();
builder.Services.AddScoped<IRegisterVerificationService, RegisterVerificationService>();
builder.Services.AddScoped<IResetVerificationService, ResetVerificationService>();

// Login Service
builder.Services.AddScoped<ILoginService, LoginService>();

// Register Service
builder.Services.AddScoped<IRegisterService, RegisterService>();

// Reset Service
builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();

// Account Service
builder.Services.AddScoped<IGetAccountService, GetAccountService>();
builder.Services.AddScoped<IUpdateAccountService, UpdateAccountService>();
builder.Services.AddScoped<IDeleteAccountService, DeleteAccountService>();

// Todo Service
builder.Services.AddScoped<IGetTodosService, GetTodosService>();
builder.Services.AddScoped<IUpdateCompleteService, UpdateCompleteService>();
builder.Services.AddScoped<ICreateTodoService, CreateTodoService>();
builder.Services.AddScoped<IGetEditService, GetEditService>();
builder.Services.AddScoped<IEditTodoService, EditTodoService>();
builder.Services.AddScoped<IDeleteTodoService, DeleteTodoService>();

// �[�J JWT ����
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

// �]�m CORS�A���\���ШD
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// ��L�A�ȵ��U
builder.Services.AddControllers();

var app = builder.Build();

// �]�m��줤����
app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5000");
