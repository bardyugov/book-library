using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookLibrary.Infrastructure.Services.Roles;

namespace BookLibrary.Infrastructure.Services.JwtService;

public class JwtService : IJwtService
{
    private const string TokenSecret = "3123n12kl3hnklo123hnl12h312l3k12lk312hlk31";
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);
    
    private string GetStringRole(List<Role> roles)
    {
        bool isFindRole = roles.Exists(role => role == Role.Admin);
        if (isFindRole) return RolesConstants.Admin;
        
        return RolesConstants.User;
    }
    
    public string GenerateToken(Author author)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new ("AuthorId", author.Id.ToString()),
            new (ClaimTypes.Role, GetStringRole(author.Roles))
        };
        
        var key = Encoding.UTF8.GetBytes(TokenSecret);
            
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifeTime),
            Issuer = "Author",
            Audience = "Audience",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
}