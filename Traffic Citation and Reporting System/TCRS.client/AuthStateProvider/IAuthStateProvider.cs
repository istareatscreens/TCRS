using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Client.AuthStateProvider
{
    public interface IAuthServiceProvider
    {
        void UnsetUser();

        void SetAuthenticatedState(UserTokens user);
    }
}
