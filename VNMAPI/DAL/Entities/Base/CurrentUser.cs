using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Base
{
    public class CurrentUser
    {
        public async Task<Employee> Get(UserManager<Employee> _userManager, HttpContext httpContext)
        {
            var currentUserName = httpContext.User.Claims.FirstOrDefault(c => c.Type.ToLower() == "nameId")?.Value;
            return await _userManager.FindByIdAsync(currentUserName);

        }
    }
}
