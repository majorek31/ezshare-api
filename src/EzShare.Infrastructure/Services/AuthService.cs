using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EzShare.Application.Contracts.Services;
using EzShare.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EzShare.Infrastructure.Services;

public class AuthService(IConfiguration configuration) : IAuthService
{
    public Task<string> GenerateJwtAsync(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JWT:PrivateKey") ?? throw new Exception("JWT:PrivateKey not present"));
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            ]),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials
        };
        var token = handler.CreateToken(tokenDescriptor);
        return Task.FromResult(handler.WriteToken(token));
    }
}