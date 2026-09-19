using BAL.DTOs;
using BAL.DTOs.Authentication.Signup;
using Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
   // [Obsolete("This interface version is deprecated. Please use v2.")]  //for test
    public interface IUserRegistrationService
    {
        Task<Response> RegisterUserAsync(RegisterUser registerUser, string role);
    }
}
