using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Models;
using ApplicationCore.Interfaces.Authentication;
using ApplicationCore.Records;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public TokenService(UserManager<ApplicationUser> userManager, IConfiguration config, ApplicationDbContext context)
        {
            _userManager = userManager;
            _config = config;
            _context = context;
        }

        public async Task<TokenPair> GenerateInitalTokenAsync(ApplicationUser user)
        {
            string accessTokenString = await GenerateAccessToken(user);
            string refreshTokenString = await GenerateRefreshTokenAsync(user);

            return new TokenPair(accessTokenString, refreshTokenString);

        }

        public async Task<TokenPair> RotateTokenAsync(string refreshToken)
        {
            RefreshToken oldToken = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (oldToken == null || oldToken.Expires < DateTime.UtcNow)
            {
                throw new ApplicationException("Token has expired");
            }

            ApplicationUser user = await _userManager.FindByIdAsync(oldToken.UserId.ToString());
            string newRefreshToken = await GenerateRefreshTokenAsync(user);
            string newAccessToken = await GenerateAccessToken(user);

            oldToken.IsRevoked = true;
            _context.Update(oldToken);
            await DeleteUnusedTokensForUser(user.Id);
            return new TokenPair(newAccessToken, newRefreshToken);
        }
        private async Task<string> GenerateRefreshTokenAsync(ApplicationUser user)
        {
            RefreshToken refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddHours(2),
                IsInUse = true,
                IsRevoked = false
            };
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            string refreshTokenString = refreshToken.Token;
            return refreshTokenString;
        }

        private async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:Exp"])),
                signingCredentials: credentials);

            string accessTokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return accessTokenString;
        }

        private async Task DeleteUnusedTokensForUser(Guid id)
        {
            await _context.RefreshTokens.Where(r => r.IsRevoked == true && r.UserId == id).ExecuteDeleteAsync();
        }
    }
}
