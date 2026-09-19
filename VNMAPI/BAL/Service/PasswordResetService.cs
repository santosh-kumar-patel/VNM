using BAL.Interface;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ApplicationDbContext _context;

        public PasswordResetService(UserManager<Employee> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> GenerateAndStoreTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var existingToken = _context.UserTokens.FirstOrDefault(ut =>
                ut.UserId == user.Id &&
                ut.LoginProvider == "Default" &&
                ut.Name == "PasswordResetToken");

            if (existingToken != null)
            {
                existingToken.Value = token;
                _context.UserTokens.Update(existingToken);
            }
            else
            {
                var userToken = new IdentityUserToken<string>
                {
                    UserId = user.Id,
                    LoginProvider = "Default",
                    Name = "PasswordResetToken",
                    Value = token
                };
                _context.UserTokens.Add(userToken);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
