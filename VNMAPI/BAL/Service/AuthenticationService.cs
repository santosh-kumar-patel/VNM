using BAL.DTOs.Authentication.Login;
using BAL.Interface;
using BAL.Validators;
using Core.Interfaces;
using Core.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public AuthenticationService(UserManager<Employee> userManager, ITokenService tokenService, IConfiguration config)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _config = config;
        }

        public async Task<AuthTokenResult> AuthenticateAsync(LoginUser loginUser)
        {
            UserValidator.ValidateLogin(loginUser);

            var user = await _userManager.FindByNameAsync(loginUser.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUser.Password))
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var tokenResult = _tokenService.GenerateToken(user, roles);

            if (tokenResult?.Claims != null)
            {
                var oldClaims = await _userManager.GetClaimsAsync(user);
                await _userManager.RemoveClaimsAsync(user, oldClaims);
                await _userManager.AddClaimsAsync(user, tokenResult.Claims);
            }

            int.TryParse(_config["JWT:RefreshTokenValidityInDays"], out int validityDays);
            user.RefreshToken = tokenResult.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(validityDays);
            user.UpdatedOn = DateTime.Now;
            user.UpdatedBy = user.Id;
            await _userManager.UpdateAsync(user);

            return tokenResult;
        }
    }
}
