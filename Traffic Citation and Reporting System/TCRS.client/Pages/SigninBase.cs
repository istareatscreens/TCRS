using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCRS.Shared.Objects;

namespace TCRS.Client.Pages
{
    public class SigninBase: ComponentBase
    {
        protected UserLoginCredentials UserCredentials { get; set; } = new UserLoginCredentials();

        protected EditContext EditContext { get; set; }

         protected bool success;

         protected string[] errors = { };

         protected override void OnInitialized()
         {      
             base.OnInitialized();
             EditContext = new EditContext(UserCredentials);
         }

         protected void Submit()
        {
              Console.WriteLine(UserCredentials.Email + " " + UserCredentials.Password);
        }
    }
}
