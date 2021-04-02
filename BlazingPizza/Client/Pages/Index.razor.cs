using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using BlazingPizza.Client.Services;

namespace BlazingPizza.Client.Pages
{
    public partial class Index 
    {
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public OrderState MyOrderState { get; set; }

        List<PizzaSpecial> Specials;        

        protected override async Task OnInitializedAsync()
        {
            Specials = await Client.GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }
        
    }
}
