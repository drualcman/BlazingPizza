using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.ComponentsLibrary.Maps
{
    partial class Map
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public List<Marker> Markers { get; set; }

        string ElementId = $"map-{Guid.NewGuid()}";

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("deliveryMap.showOrUpdate", ElementId, Markers);
        }
    }
}
