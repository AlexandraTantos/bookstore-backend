using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookStore.Abstraction;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Services
{
    public class Auth : IAuth
    {
        private readonly IAuthSecuredKey authSecuredKey;
        private readonly List<string> blacklistedAccessTokens = new();
        private readonly List<string> blacklistedRefreshTokens = new();
        private readonly Dictionary<string, (string userId, string role)> refreshTokenStore = new();

        public Auth(IAuthSecuredKey authSecuredKey)
        {
            this.authSecuredKey = authSecuredKey;
        }

        public string GenerateAccessToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authSecuredKey.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15), 
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(string userId, string role)
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            var refreshToken = Convert.ToBase64String(randomBytes);

            refreshTokenStore[refreshToken] = (userId, role);
            return refreshToken;
        }

        public (string accessToken, string refreshToken)? GetNewPairs(string refreshToken)
        {
            if (blacklistedRefreshTokens.Contains(refreshToken))
                return null;
            
            if (refreshTokenStore.TryGetValue(refreshToken, out var info))
            {
                string userId = info.userId;
                string role = info.role;

                refreshTokenStore.Remove(refreshToken);
                blacklistedRefreshTokens.Add(refreshToken);

                var newAccessToken = GenerateAccessToken(userId, role);
                var newRefreshToken = GenerateRefreshToken(userId, role);

                return (newAccessToken, newRefreshToken);
            }

            return null;
        }

        public bool ValidateRole(string accessToken, string requiredRole)
        {
            if (blacklistedAccessTokens.Contains(accessToken))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(accessToken);

            var roleClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            return roleClaim != null && roleClaim.Value == requiredRole;
        }

        public void BlackListToken(string accessToken)
        {
            if (!blacklistedAccessTokens.Contains(accessToken))
                blacklistedAccessTokens.Add(accessToken);
        }

        public void BlackListRefreshToken(string refreshToken)
        {
            if (!blacklistedRefreshTokens.Contains(refreshToken))
                blacklistedRefreshTokens.Add(refreshToken);

            if (refreshTokenStore.ContainsKey(refreshToken))
                refreshTokenStore.Remove(refreshToken);
        }
    }
}
