using Eventinho.Domain.Entities;
using Eventinho.Domain.Interfaces.Repository;
using Eventinho.Shared.Configuration;
using Eventinho.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Eventinho.Shared.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly TokenConfig _config;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IOptions<TokenConfig> config, IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _config = config.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateUserAsync(string userName, string email, string password)
        {
            var repository = _unitOfWork.Repository<User>();

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = userName,
                    Email = email,
                    PasswordHash = GetHashPassword(password),
                };

                var existingEmail = await repository.Entities.SingleOrDefaultAsync(u => u.Email == email);

                if (existingEmail != null)
                    throw new InvalidOperationException("Email already exists.");

                await repository.AddAsync(user);

                var token = GenerateToken(user);

                await _unitOfWork.CommitAsync();

                return token;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();

                throw;
            }

        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.Entities.SingleOrDefaultAsync(u => u.Email == email && u.PasswordHash == GetHashPassword(password))
                ?? throw new InvalidOperationException("Invalid email or password.");

            return GenerateToken(user);
        }
        private string GenerateToken(User user)
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

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.Entities.SingleOrDefaultAsync(i => i.Email == email)
                ?? throw new Exception("User not found");

                return user;
        }

        private ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            return ci;
        }

        private string GetHashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}