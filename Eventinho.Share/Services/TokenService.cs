using Eventinho.Domain.Entities;
using Eventinho.Shared.Configuration;
using Eventinho.Shared.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eventinho.Shared.Services
{
    public class TokenService(IOptions<TokenConfig> config) : ITokenService
    {
        private readonly TokenConfig _config = config.Value;

        public string Generate(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config.Key);

            var credentials = new SigningCredentials(
                                                      new SymmetricSecurityKey(key),
                                                      SecurityAlgorithms.HmacSha256Signature
                                                    );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            return ci;
        }
    }
}
