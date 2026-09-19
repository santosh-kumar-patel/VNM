using Core.Contstants;
using Core.Interfaces;
using Core.Models;
using DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _config = configuration;
            _logger = logger;

        }
        public AuthTokenResult GenerateToken(Employee _user, IList<string> roles)
        {
            try
            {
                if (_user == null || roles == null)
                {
                    _logger.LogWarning("Token generation failed due to null user or roles.");
                    return new AuthTokenResult();
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(AppClaims.UserId, _user.Id),
                        new Claim(AppClaims.Name, _user.UserName==null?"":_user.UserName),
                        new Claim(AppClaims.Email, _user.Email==null?"":_user.Email),
                        new Claim(AppClaims.Jti, Guid.NewGuid().ToString())
                    };

                   
                    foreach (var role in roles)
                        claims.Add(new Claim(AppClaims.Role, role));
                  
                    var token = CreateToken(claims);
                    var refreshToken = GenerateRefreshToken();

                    return new AuthTokenResult
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo,
                        Claims = claims
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Message: " + ex.Message,"StackTrace : " + ex.StackTrace);
            }
            return null;
        }

        public JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            if (_config["JWT:SecretKey"] != null)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
                _ = int.TryParse(_config["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return token;
            }
            return new JwtSecurityToken();
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            if (IsJwtConfigurationValid())
            {
                try
                {
                    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                    if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                        throw new SecurityTokenException("Invalid token");
                    return principal;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to extract principal from expired token.");
                    return null;
                }
            }

            return null;
        }

        private bool IsJwtConfigurationValid()
        {
            return !string.IsNullOrEmpty(_config["JWT:SecretKey"])
                && !string.IsNullOrEmpty(_config["JWT:Issuer"])
                && !string.IsNullOrEmpty(_config["JWT:Audience"])
                && int.TryParse(_config["JWT:TokenValidityInMinutes"], out _);
        }

    }
}

