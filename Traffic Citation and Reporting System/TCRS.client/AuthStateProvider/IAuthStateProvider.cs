using System.Threading.Tasks;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Client.AuthStateProvider
{
    public interface IAuthServiceProvider
    {
        Task UnsetUser();

        void SetAuthenticatedState(UserTokens user);
    }
}
