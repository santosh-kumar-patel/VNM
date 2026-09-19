using BAL.DTOs.Authentication.Login;
using BAL.DTOs.Authentication.Signup;
using Core.Contstants;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validators
{
    public static class UserValidator
    {

        //use this BAL
        public static void ValidateLogin(LoginUser request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
                throw new BusinessRuleException("UserName is required.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new BusinessRuleException("Password is required.");

            //if (!request.Email.Contains("@"))
            //    throw new BusinessRuleException("Invalid email format.");
        }

        public static void ValidateRegister(RegisterUser registerUser)
        {
            if (string.IsNullOrWhiteSpace(registerUser.UserName))
                throw new BusinessRuleException("UserName is required.");

            if (string.IsNullOrWhiteSpace(registerUser.Password))
                throw new BusinessRuleException("Password is required.");

            if (!registerUser.Email.Contains("@"))
                throw new BusinessRuleException("Invalid email format.");
        }

        public static void ValidateRoleAssignment(string role)
        {
            if (!AppRoles.AllRoles.Select(x=>x.ToLower()).Contains(role.ToLower()))
                throw new BusinessRuleException($"Role '{role}' is not recognized.");
        }
    }

}
