using System.Text;
using BookLibrary.Application.Commands.Authors;
using BookLibrary.Application.Repositories;
using BookLibrary.Application.Services;
using BookLibrary.Infrastructure;
using BookLibrary.Infrastructure.Commands.Authors;
using BookLibrary.Infrastructure.Repositories;
using BookLibrary.Infrastructure.Services.JwtService;
using BookLibrary.Infrastructure.Services.PasswordService;
using BookLibrary.Infrastructure.Services.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseNpgsql(config["URI"]));

builder.Services.AddMediatR(x => 
    x.RegisterServicesFromAssemblies(typeof(CreateAuthorHandler).Assembly, typeof(CreateAuthorCommand).Assembly));

builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x
        => x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
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

var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();