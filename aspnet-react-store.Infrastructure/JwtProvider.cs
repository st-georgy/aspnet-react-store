using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using aspnet_react_store.Domain.Abstractions.Auth;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Infrastructure
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public string GenerateToken(User user)
        {
            Claim[] claims = [
                new("userId", user.Id.ToString()),
                new("role", user.UserRole.ToString()),
                new("userName", user.UserName),
                new("email", user.Email)
                ];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
