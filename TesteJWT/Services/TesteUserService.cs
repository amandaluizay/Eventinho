using Eventinho.Domain.Entities;
using Eventinho.Domain.Interfaces.Repository;
using Eventinho.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteJWT.Interfaces;

namespace TesteJWT.Services
{
    public class TesteUserService : ITesteUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly TokenConfig _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly 

        public TesteUserService(IOptions<TokenConfig>config, IRepository<User> userRepository, IHttpContextAccessor httpContextAccessor )
        {
            _userRepository = userRepository;
            _config = config.Value;
            _httpContextAccessor = httpContextAccessor;
        }
         
        public ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            return ci;
        }

        public string GenerateToken(User user)
        {
           var handler = new JwtSecurityTokenHandler();
           var key = Encoding.ASCII.GetBytes(_config.Key);

            var credentials = new SigningCredentials(
                                                      new SymmetricSecurityKey(key),
                                                      SecurityAlgorithms.HmacSha256
                                                     );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(_config.ExpirationMinutes),
                SigningCredentials = credentials
            };
            
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);

        }

        public async Task<string> CreateUserAsync(string userName, string email, string password)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email,
                PasswordHash = Encoding.ASCII.GetBytes(password).ToString(),
                CreatedBy = userName
            };

            var existingEmail = await _userRepository.Entities.SingleOrDefaultAsync(u => u.Email == email);
            if (existingEmail != null)
                throw new InvalidOperationException("Email already exists.");

            await _userRepository.AddAsync(user);

            return GenerateToken(user);
        }


        public string GetCurrentUserEmail()
        {
            string? Email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            return string.IsNullOrEmpty(Email) ? "system without user" : Email;
        }

        public string GetCurrentUserName()
        {
           string? Name = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
           return string.IsNullOrEmpty(Name) ? "system without user" : Name;
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.Entities.SingleOrDefaultAsync(u => u.Email == email && u.PasswordHash == Encoding.ASCII.GetBytes(password).ToString())
                ?? throw new InvalidOperationException("Invalid email or password.");

            return GenerateToken(user);
        }
    }
}
