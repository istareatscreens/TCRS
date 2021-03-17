using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TCRS.Business;
using TCRS.Shared.Contracts;

namespace TCRS.Client.Shared
{
    public class NavBarBase : ComponentBase
    {
        [Inject] 
        private IUserService User { get; set; }
        protected void SignOut()
        {
            Console.WriteLine((User.User == null)?"":User.User.email);
        }
    }
}
