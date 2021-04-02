using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public partial class ConfiguredPizzaItem
    {
        [Parameter]
        public Pizza MyPizza { get; set; }

        [Parameter]
        public EventCallback OnRemoved { get; set; }

        [Parameter]
        public EventCallback OnEdit { get; set; }

        bool ShowingConfigureDialog = false;

        void Edit()
        {
            ShowingConfigureDialog = true;
            OnEdit.InvokeAsync();
        }

        void Confirm()
        {
            ShowingConfigureDialog = false;
        }

    }
}
