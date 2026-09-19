using BAL.Interface;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service
{
    public class TokenRefreshService : ITokenRefreshService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<TokenRefreshService> _logger;

        public TokenRefreshService(
            UserManager<Employee> userManager,
            ITokenService tokenService,
            ILogger<TokenRefreshService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<AuthTokenResult> RefreshAsync(TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                _logger.LogWarning("TokenModel is null");
                return null;
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
            if (principal?.Identity is not ClaimsIdentity identity)
            {
                _logger.LogWarning("Invalid token structure");
                return null;
            }

            var userId = identity.Claims.FirstOrDefault(c => c.Type.Equals("user_id", StringComparison.OrdinalIgnoreCase))?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID claim missing");
                return null;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _logger.LogWarning("Refresh token validation failed");
                return null;
            }

            var newAccessToken = _tokenService.CreateToken(identity.Claims.ToList());
            var newRefreshToken = TokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.UpdatedOn = DateTime.Now;
            user.UpdatedBy = user.Id;

            await _userManager.UpdateAsync(user);

            return new AuthTokenResult
            {
                Success = true,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo,
                Claims = identity.Claims.ToList(),
                StatusCode = StatusCodes.Status200OK,
                Message="Refresh token successfully.",
                Timestamp = DateTime.UtcNow,
            };
        }
    }
}
