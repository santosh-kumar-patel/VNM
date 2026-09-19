using BAL.DTOs;
using BAL.DTOs.Authentication.Login;
using BAL.DTOs.Authentication.Signup;
using Core.Services;
using BAL.Validators;
using Core.Enums;
using Core.Models;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BAL.Interface;
using BAL.Service;
using Core.Models.Base;

namespace VNMAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthenticationService _authService;
        private readonly IPasswordResetService _passwordResetService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly ITokenRefreshService _tokenRefreshService;

        public AuthController(
        UserManager<Employee> userManager,
        ILogger<AuthController> logger, ApplicationDbContext context, 
        IAuthenticationService authService, IPasswordResetService passwordResetService, 
        IUserRegistrationService userRegistrationService, ITokenRefreshService tokenRefreshService)
        {
            _userManager = userManager;
            _logger = logger;
            _context= context;
            _authService = authService;
            _passwordResetService = passwordResetService;
            _userRegistrationService = userRegistrationService;
            _tokenRefreshService = tokenRefreshService;
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            var result = await _userRegistrationService.RegisterUserAsync(registerUser, role);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Login")]
        //[Consumes("application/json")]
        //[Produces("application/json", "application/xml")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tokenResult = await _authService.AuthenticateAsync(loginUser);
                    if (tokenResult == null)
                        return StatusCode(StatusCodes.Status404NotFound, new Response
                        {
                            Success = false,
                            Message = "User not found!",
                            StatusCode = StatusCodes.Status404NotFound,
                            Timestamp = DateTime.UtcNow,
                        });
                    return Ok(tokenResult);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode= StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                });
            }
        }


        [HttpPost("PasswordResetToken")]
        public async Task<IActionResult> GeneratePasswordResetToken(string userId)
        {
            try
            {
                var success = await _passwordResetService.GenerateAndStoreTokenAsync(userId);
                if (!success)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Response
                    {
                        Success = false,
                        Message = "Reset token not Found",
                        StatusCode = StatusCodes.Status404NotFound,
                        Timestamp = DateTime.UtcNow,
                    }) ;
                }

                return StatusCode(StatusCodes.Status200OK,new Response
                {
                    Success = true,
                    Message = "Password Reset Token Generated Successfully!",
                    StatusCode = StatusCodes.Status200OK,
                    Timestamp = DateTime.UtcNow,
                }); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                });
            }
        }

        [HttpPost("EmailConfirmationToken")]
        public async Task<IActionResult> GenerateEmailConfirmationToken(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var userToken = new IdentityUserToken<string>
                    {
                        UserId = user.Id,
                        LoginProvider = "Default",
                        Name = "EmailConfirmationToken",
                        Value = token
                    };

                    _context.UserTokens.Add(userToken);
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, new Response
                    {
                        Success = true,
                        Message = "Email Confirmation Token Generate Successfully!",
                        StatusCode = StatusCodes.Status200OK,
                        Timestamp = DateTime.UtcNow,
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response
                    {
                        Success = true,
                        Message = "user ID not found",
                        StatusCode = StatusCodes.Status400BadRequest,
                        Timestamp = DateTime.UtcNow,
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                });
            } 
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var authTokenResult = await _tokenRefreshService.RefreshAsync(tokenModel);

            if (authTokenResult == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Success = false,
                    Message = "Invalid access token or refresh token",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Timestamp = DateTime.UtcNow,
                });
            }

            return Ok(authTokenResult);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) return BadRequest("Invalid user name");

                user.RefreshToken = null;
                user.UpdatedOn = DateTime.Now;
                user.UpdatedBy = user.Id;
                await _userManager.UpdateAsync(user);

                return StatusCode(StatusCodes.Status200OK, new Response
                {
                    Success = true,
                    Message = "Revoked (" + username + ")  successfully",
                    StatusCode = StatusCodes.Status200OK,
                    Timestamp = DateTime.UtcNow,
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                });
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            try
            {
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    user.RefreshToken = null;
                    user.UpdatedOn = DateTime.Now;
                    user.UpdatedBy = user.Id;
                    await _userManager.UpdateAsync(user);
                }

                return StatusCode(StatusCodes.Status200OK, new Response
                {
                    Success = true,
                    Message = "Revoked all successfully",
                    StatusCode = StatusCodes.Status200OK,
                    Timestamp = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                });
            }
           
        }
    }

}
