using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.TemplateComponents
{
    partial class TemplateList<TItem> : ComponentBase
    {
        [Parameter]
        public Func<Task<List<TItem>>> Loader { get; set; }

        [Parameter]
        public RenderFragment Loading { get; set; }

        [Parameter]
        public RenderFragment Empty { get; set; }

        [Parameter]
        public RenderFragment<TItem> Item { get; set; }

        [Parameter]
        public string ListGroupClass { get; set; }


        List<TItem> Items;

        protected override void OnParametersSet()
        {
            if (Loading is null)
            {
                
                Loading = builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddAttribute(0, "class", "loading-bar");
                    builder.CloseElement();
                    builder.OpenElement(1, "p");
                    builder.AddContent(1, "...");
                    builder.CloseElement();
                };
            }
            if (Empty is null)
            {
                Empty = builder =>
                {
                    builder.OpenElement(0, "h2");
                    builder.AddContent(0, "Nothing here yet!");
                    builder.CloseElement();
                    builder.OpenElement(1, "a");
                    builder.AddAttribute(1, "href", "\\");
                    builder.AddContent(1, "< Back");
                    builder.CloseElement();
                };

                
            }

        }

        protected override async Task OnParametersSetAsync()
        {
            Items = await Loader();           
        }
    }
}
