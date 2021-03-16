﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TCRS.Shared.Objects.Login;
using TCRS.Business;
using TCRS.Shared.Contracts;

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

        [Inject]
        private IUserManager UserManager { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

         protected async void HandleSubmit()
         {
             if (!EditContext.Validate())
             {
                 return;
             }

             var userFound = await UserManager.UserSignIn(UserCredentials);
             if (userFound != null)
             {
                NavigationManager.NavigateTo(("Placeholder"));
             }
        }
    }
}
