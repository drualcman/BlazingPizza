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


        protected override async Task OnParametersSetAsync()
        {
            Items = await Loader();

            RenderFragment treeBuilder;

            treeBuilder = builder =>
            {
                builder.OpenElement(0, "loader");
                builder.AddContent(0, "<div class=\"loading-bar\"></div>");
                builder.CloseElement();
            };

            //RenderTreeBuilder treeBuilder = new RenderTreeBuilder();            
            if (Loading is null)
            {
                //Loading = treeBuilder;
                //treeBuilder.Clear();
                //treeBuilder.AddMarkupContent(0, "<div class=\"loading-bar\"></div>");
                Loading = builder =>
                {
                    builder.OpenElement(0, "loader");
                    builder.AddContent(0, "<div class=\"loading-bar\"></div>");
                    builder.CloseElement();
                };
            }
            //if (Empty is null)
            //{
            //    treeBuilder.Clear();
            //    treeBuilder.AddMarkupContent(0, "<h2>No orders yet!</h2>");
            //    treeBuilder.AddMarkupContent(1, "<a href=\"\" class=\"btn btn-success\">Get a pizza!</a>");
            //    Empty = builder =>
            //    {
            //        builder.OpenComponent(0, typeof(TItem));
            //        builder.AddMarkupContent(0, "<h2>No orders yet!</h2>");
            //        builder.AddMarkupContent(1, "<a href=\"\" class=\"btn btn-success\">Get a pizza!</a>");
            //        builder.CloseComponent();
            //    };
            //}

        }
    }
}
