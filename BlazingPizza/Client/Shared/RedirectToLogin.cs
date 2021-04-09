using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public class RedirectToLogin: ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        protected override void OnInitialized()
        {
            string EscapedUri = Uri.EscapeDataString(Navigation.Uri);
            Navigation.NavigateTo($"authentication/login?returnUrl={EscapedUri}");
        }
    }
}
