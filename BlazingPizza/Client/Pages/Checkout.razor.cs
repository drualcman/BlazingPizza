using BlazingPizza.Client.Services;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;
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
        
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        bool Clicked = false;

        protected override void OnInitialized()
        {
            _ = RequestNotificationSubscriptionAsync();
        }

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

        async Task RequestNotificationSubscriptionAsync()
        {
            NotificationSubscription subscription = await JsRuntime.InvokeAsync<NotificationSubscription>("blazorPushNotifications.requestSubscription");
            if (subscription is not null)
            {
                try
                {
                    await Client.SubscribeNotifications(subscription);
                }
                catch (AccessTokenNotAvailableException ex)
                {
                    ex.Redirect();
                }
            }
        }
    }
}
