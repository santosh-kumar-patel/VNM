using Core.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        public AuthTokenResult GenerateToken(Employee _user, IList<string> roles);
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
