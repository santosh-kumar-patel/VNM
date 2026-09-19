using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface ITokenRefreshService
    {
        Task<AuthTokenResult> RefreshAsync(TokenModel tokenModel);
    }
}
