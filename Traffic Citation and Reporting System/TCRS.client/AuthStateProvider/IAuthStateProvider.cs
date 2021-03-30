using TCRS.Shared.Objects.Auth;

namespace TCRS.Client.AuthStateProvider
{
    public interface IAuthServiceProvider
    {
        void UnsetUser();

        void SetAuthenticatedState(UserTokens user);
    }
}
