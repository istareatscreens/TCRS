using System;
using System.Collections.Generic;
using System.Text;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Login;

namespace TCRS.Business
{
    public class UserService : IUserService
    {
        public UserWithToken User { get; set; }
    }
}
