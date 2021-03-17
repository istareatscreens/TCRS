using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Login;

namespace TCRS.Shared.Contracts
{
    public interface IPersistanceService
    {
        Task<UserWithToken> AuthenticateAndGetUserAsync(UserLoginCredentials user, IPersistanceService api);
    }
}
