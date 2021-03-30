using System.Threading.Tasks;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Shared.Contracts
{
    public interface IUserManager
    {
        Task<UserTokens> UserSignIn(UserLoginCredentials userLoginCredentials);
    }
}
