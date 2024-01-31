using BookLibrary.Application.Services;
using BookLibrary.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookLibrary.Infrastructure.Services.JwtService;

public class JwtService : IJwtService
{
    private const string ClaimKey = "AuthorId";
    private readonly string _tokenSecret = "Keydasdasdasdasdasd12312312dewqxasddasd";
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);

    public JwtService(IConfigService configService)
    {
        var key = configService.GetValue("JwtSettings:Key");
        if (key != null) _tokenSecret = key;
    }
    
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
        
        var key = Encoding.UTF8.GetBytes(_tokenSecret);
            
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