using APIMMA.BackgroundJobs.Emails;
using APIMMA.Data;
using APIMMA.Exceptions;
using APIMMA.Factories;
using APIMMA.Services;
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add connection string and database context
builder.Services.AddDbContext<AppDbContext>(
    context => context.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure hangfire with SQL Server storage
builder.Services.AddHangfire(options => options.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();

builder.Services.AddScoped<IEmailService, EmailService>();

// Background Jobs services
builder.Services.AddScoped<IEmailJobs, EmailJobs>();

// Factories
builder.Services.AddSingleton<SendGridFactory>();

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

app.UseCors("AllowFrontend");

// Configure Hangfire Dashboard (optional, for monitoring background jobs)
app.UseHangfireDashboard();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
