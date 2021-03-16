using System;
using System.Collections.Generic;
using System.Text;
using TCRS.Shared.Objects.Login;

namespace TCRS.Shared.Contracts
{
    public interface IUserService
    {
        UserWithToken User { get; set; }    
    }
}
