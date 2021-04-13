using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Authentication
    {
        [Parameter]
        public string action { get; set; }

        [Inject]
        public OrderState MyOrderState { get; set; }

        public PizzaAuthenticationState RemoteAuthenticationState { get; set; } = new PizzaAuthenticationState();

        protected override void OnInitialized()
        {
            if (RemoteAuthenticationActions.IsAction(RemoteAuthenticationActions.LogIn, action))
            {
                RemoteAuthenticationState.MyOrder = MyOrderState.MyOrder;
            }
        }

        void RestorePizza(PizzaAuthenticationState pizzaAuthenticationState)
        {
            if (pizzaAuthenticationState.MyOrder is not null)
                MyOrderState.ReplaceOrder(pizzaAuthenticationState.MyOrder);
        }

    }
}
