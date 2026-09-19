using BAL.DTOs.Authentication.Login;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IAuthenticationService
    {
        Task<AuthTokenResult> AuthenticateAsync(LoginUser loginUser);
    }

}
