using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazingPizza.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }

        List<PizzaSpecial> Specials;

        protected override async Task OnInitializedAsync()
        {
            Specials = await Client.GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }
    }
}
