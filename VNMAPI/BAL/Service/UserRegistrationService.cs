using BAL.DTOs;
using BAL.DTOs.Authentication.Login;
using BAL.DTOs.Authentication.Signup;
using BAL.Interface;
using BAL.Validators;
using Core.Models.Base;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly ILogger<UserRegistrationService> _logger;

        public UserRegistrationService(
            UserManager<Employee> userManager,
            RoleManager<UserRole> roleManager,
            ILogger<UserRegistrationService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<Response> RegisterUserAsync(RegisterUser registerUser, string role)
        {
            try
            {
                UserValidator.ValidateRegister(registerUser);

                UserValidator.ValidateRoleAssignment(role);
                
                if (registerUser.Password != registerUser.ConfirmPassword)
                    return new Response 
                    { 
                        Success = false, 
                        Message = "Passwords do not match!", 
                        StatusCode = StatusCodes.Status400BadRequest,
                        Timestamp = DateTime.UtcNow
                    };

                var userEmailExist = await _userManager.FindByEmailAsync(registerUser.Email);
                var userNameExist = await _userManager.FindByNameAsync(registerUser.UserName);
                if (userEmailExist != null || userNameExist != null)
                    return new Response 
                    { 
                        Success = false, 
                        Message = "User already exists!", 
                        StatusCode = StatusCodes.Status403Forbidden,
                        Timestamp = DateTime.UtcNow
                    };

                var user = new Employee
                {
                    UserName = registerUser.UserName,
                    Email = registerUser.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CreatedBy = "1"
                };

                if (!await _roleManager.RoleExistsAsync(role))
                    return new Response 
                    { 
                        Success = false, 
                        Message = "This role does not exist!", 
                        StatusCode = StatusCodes.Status400BadRequest, 
                        Timestamp = DateTime.UtcNow
                    };

                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                    return new Response 
                    { 
                        Success = false, 
                        Message = "User failed to create!", 
                        StatusCode = StatusCodes.Status400BadRequest, 
                        Timestamp = DateTime.UtcNow
                    };

                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                if (currentUser != null)
                {
                    currentUser.CreatedBy = currentUser.Id;
                    await _userManager.UpdateAsync(currentUser);
                }

                await _userManager.AddToRoleAsync(user, role);

                return new Response 
                { 
                    Success = true, 
                    Message = "User Created Successfully!", 
                    StatusCode = StatusCodes.Status201Created, 
                    Timestamp = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                return new Response
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Timestamp = DateTime.UtcNow,
                };
            }
        }
    }
}
