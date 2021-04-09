using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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
        public OrdersClient Client { get; set; }

        bool Clicked = false;

        async Task PlaceOrder()
        {
            if (!Clicked)
            {
                Clicked = true;
                try
                {
                    int newOrderId = await Client.PlaceOrder(MyOrderState.MyOrder);
                    MyOrderState.ResetOrder();
                    Navigation.NavigateTo($"/myorders/{newOrderId}");
                }
                catch (AccessTokenNotAvailableException ex)
                {
                    ex.Redirect();
                }
            }
        }
    }
}
