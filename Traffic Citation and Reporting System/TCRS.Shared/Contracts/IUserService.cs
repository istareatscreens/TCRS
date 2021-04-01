using TCRS.Shared.Objects.Auth;

namespace TCRS.Shared.Contracts
{
    public interface IUserService
    {
        public User User { get; set; }
        string GetFullName();
    }
}
