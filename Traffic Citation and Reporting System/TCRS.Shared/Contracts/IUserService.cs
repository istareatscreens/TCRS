using System;
using System.Collections.Generic;
using System.Text;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Shared.Contracts
{
    public interface IUserService
    {
        public User User { get; set; }    
    }
}
