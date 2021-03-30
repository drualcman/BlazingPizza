using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Myorders
    {
        [Inject]
        public HttpClient Client { get; set; }

        List<OrderWithStatus> MyOrdersWithStatus;

        protected override async Task OnParametersSetAsync()
        {
            MyOrdersWithStatus = await Client.GetFromJsonAsync<List<OrderWithStatus>>("orders");
        }
    }
}
