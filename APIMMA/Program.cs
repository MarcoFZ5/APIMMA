using APIMMA.Data;
using APIMMA.Dtos;
using APIMMA.Exceptions;
using APIMMA.Services;
using APIMMA.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add connection string and database context
var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    context => context.UseSqlServer(_connectionString));

var _secretKey = builder.Configuration.GetSection("Jwt:Key").Value;

builder.Services.AddAuthentication().AddJwtBearer("Bearer", auth => {

    auth.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "APIMMA",
        ValidAudience = "FRONTAPP",
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_secretKey))
    };
    }
);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add Validators to the container.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
//builder.Services.AddScoped(typeof(ValidationFilter<>)); FOR FUTURE TO DO

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
