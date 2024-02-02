using System.Text;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Infrastructure;
using BookLibrary.Infrastructure.Repositories;
using BookLibrary.Infrastructure.Services.ConfigService;
using BookLibrary.Infrastructure.Services.JwtService;
using BookLibrary.Infrastructure.Services.PasswordService;
using BookLibrary.Core.Behavior;
using BookLibrary.Core.Middlewares;
using BookLibrary.Infrastructure.Services.AuthenticationService;
using BookLibrary.Infrastructure.Services.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using MediatR;

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseNpgsql(config["ConnectionStrings:URI"]));

builder.Services.AddMediatR(x => 
    x.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IConfigService, ConfigService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x
        => x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JwtSettings:Key"] is null ? "Keydasdasdasdasdasd12312312dewqxasddasd" : config["JwtSettings:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RolesConstants.User, rolePolice =>
    {
        rolePolice.RequireRole(RolesConstants.User, RolesConstants.Admin);
    });
        
    options.AddPolicy(RolesConstants.Admin, rolePolice =>
    {
        rolePolice.RequireRole(RolesConstants.Admin);
    });
});

builder.Services.AddValidatorsFromAssemblies(assemblies);

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapControllers();
app.Run();