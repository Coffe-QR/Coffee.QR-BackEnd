using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;
using FluentResults;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Coffee.QR.Core.Services;
using System.Security.Cryptography;

namespace Coffee.QR.Infrastructure.Auth
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly string _key = Environment.GetEnvironmentVariable("JWT_KEY") ?? "coffeeQR_secret_key";
        private readonly string _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "coffeeQR";
        private readonly string _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "coffeeQR-front.com";

        public JwtGenerator()
        {
            // Generisanje ključa samo za demostracione svrhe. Za produkciju, koristite fiksni ključ definisan u konfiguraciji ili varijablama okruženja.
            _key = GenerateSecureKey();
        }


        public Result<AuthenticationTokensDto> GenerateAccessToken(User user)
        {
            var authenticationResponse = new AuthenticationTokensDto();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("id", user.Id.ToString()),
                new("username", user.Username),
                new("role", user.GetPrimaryRoleName())
            };

            var jwt = CreateToken(claims, 60 * 24);
            authenticationResponse.Id = user.Id;
            authenticationResponse.AccessToken = jwt;

            return authenticationResponse;
        }

        private string CreateToken(IEnumerable<Claim> claims, double expirationTimeInMinutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateSecureKey()
        {
            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            var randomBytes = new byte[32]; // 32 bajta = 256 bita
            randomNumberGenerator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

    }
}
