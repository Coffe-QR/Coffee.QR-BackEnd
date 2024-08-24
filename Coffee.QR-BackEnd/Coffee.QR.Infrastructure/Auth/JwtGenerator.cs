using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Domain;
using FluentResults;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Coffee.QR.Core.Services;
using System.Security.Cryptography;
using Coffee.QR.API.Public;
using System.Text.Json;

namespace Coffee.QR.Infrastructure.Auth
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IUserService _userService;
        private const double dayInMinutes = 60 * 24;
        private const string idPlaceholder = "id";
        private const string usernamePlaceholder = "username";

        public JwtGenerator(IUserService userService)
        {
            _userService = userService;
            string filePath = "../Coffee.QR-BackEnd/Resources/appJwtSettings.json";
            string jsonString = File.ReadAllText(filePath);
            JWTCredentialsDto credentials = JsonSerializer.Deserialize<JWTCredentialsDto>(jsonString);

            _key = Environment.GetEnvironmentVariable("JWT_KEY") ?? credentials.Key;
            _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? credentials.Issuer;
            _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? credentials.Audience;
        }


        public Result<AuthenticationTokensDto> GenerateAccessToken(User user)
        {
            var authenticationResponse = new AuthenticationTokensDto();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(idPlaceholder, user.Id.ToString()),
                new(usernamePlaceholder, user.Username),
            };

            var jwt = CreateToken(claims, dayInMinutes);
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
