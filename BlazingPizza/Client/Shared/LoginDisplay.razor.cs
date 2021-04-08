using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public partial class LoginDisplay
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public SignOutSessionStateManager SignOutManager { get; set; }

        async Task BeginSignOut()
        {
            await SignOutManager.SetSignOutState();
            Navigation.NavigateTo("authentication/logout");
        }
    }
}
