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
    public partial class Index 
    {
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        List<PizzaSpecial> Specials;
        bool ShowingConfigureDialog;
        Pizza ConfiguringPizza;
        Order MyOrder;

        protected override async Task OnInitializedAsync()
        {
            MyOrder = new Order();          //for performance can be here
            Specials = await Client.GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }

        void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>()
            };
            ShowingConfigureDialog = true;
        }

        void OnCancel_Click()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }

        void OnConfirm_Click()
        {
            MyOrder.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }

        void OnRemoved_Click(Pizza pizza)
        {
            MyOrder.Pizzas.Remove(pizza);
        }

        async Task PlaceOrder()
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("orders", MyOrder);
            int newOrderId = await response.Content.ReadFromJsonAsync<int>();
            MyOrder = new Order();
            Navigation.NavigateTo($"/myorders/{newOrderId}");
        }
    }
}
