using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.TemplateComponents
{
    partial class TemplateDialog
    {
        /// <summary>
        /// If only have one content to receive must be call this property with this name.
        /// If you will receive more than one content then name most be descriptive
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Show { get; set; }

    }
}
