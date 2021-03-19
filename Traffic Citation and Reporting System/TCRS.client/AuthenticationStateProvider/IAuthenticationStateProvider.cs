using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Client.AuthenticationStateProvider
{
    public interface IAuthenticationStateProvider
    {
        void UnsetUser();

        void SetAuthenticatedState(UserTokens user);
    }
}
