using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SimpleWebApp.Models;

namespace SimpleWebApp.Services
{
    public interface IAuthService
    {
        string GenerateJwt(User user);
        ClaimsPrincipal ValidateJwt(string token);
        int? GetUserId(ClaimsPrincipal principal);
        string GetRole(ClaimsPrincipal principal);
    }

    public class AuthService : IAuthService
    {
        // In a real app, store securely in config or environment variable
        private readonly string _jwtSecret = "ThisIsASuperStrongJWTSecretKey123!";

        // ==================== JWT Generation ====================
        public string GenerateJwt(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        // ==================== JWT Validation ====================
        public ClaimsPrincipal ValidateJwt(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero // no tolerance window
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, parameters, out var validatedToken);

                // Ensure token was signed with correct algorithm
                if (validatedToken is JwtSecurityToken jwt &&
                    jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
                {
                    return principal;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // ==================== Claims Helpers ====================
        public int? GetUserId(ClaimsPrincipal principal)
        {
            var idClaim = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(idClaim, out var id) ? id : (int?)null;
        }

        public string GetRole(ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
