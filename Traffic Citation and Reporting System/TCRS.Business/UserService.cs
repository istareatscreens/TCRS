using System;
using System.Collections.Generic;
using System.Text;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Business
{
    public class UserService : IUserService
    {
        public User User { get; set; }
    }
}
