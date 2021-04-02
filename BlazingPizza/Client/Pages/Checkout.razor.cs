using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Checkout
    {
        [Inject]
        public OrderState MyOrderState { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        bool Clicked = false;

        async Task PlaceOrder()
        {
            if (!Clicked)
            {
                Clicked = true;
                HttpResponseMessage response = await Client.PostAsJsonAsync("orders", MyOrderState.MyOrder);
                int newOrderId = await response.Content.ReadFromJsonAsync<int>();
                MyOrderState.ResetOrder();
                Navigation.NavigateTo($"/myorders/{newOrderId}");
            }
        }
    }
}
