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
    private const string ClaimKey = "AuthorId";
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);
    
    public string GenerateToken(Author author)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new (ClaimKey, author.Id.ToString()),
        };
        
        foreach (var role in author.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }
        
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