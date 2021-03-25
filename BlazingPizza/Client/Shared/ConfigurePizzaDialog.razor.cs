using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public partial class ConfigurePizzaDialog
    {
        [Parameter]
        public Pizza MyPizza { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        [Parameter]
        public EventCallback OnConfirm { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        List<Topping> Toppings;

        protected override async Task OnInitializedAsync()
        {
            Toppings = await Client.GetFromJsonAsync<List<Topping>>("toppings");
        }

        void AddTopping(Topping topping)
        {
            if (MyPizza.Toppings.Find(pt => pt.Topping == topping) is null)
            {
                MyPizza.Toppings.Add(new PizzaTopping
                {
                    Topping = topping
                });
            }
        }

        void ToppingSelected(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out int index) && index >= 0)
                AddTopping(Toppings[index]);
        }

        void RemoveTopping(Topping topping)
        {
            MyPizza.Toppings.RemoveAll(pt => pt.Topping == topping);
        }

        bool ContainsTopping(Topping topping)
        {
            bool result = false;

            foreach (PizzaTopping item in MyPizza.Toppings)
            {
                if (item.Topping == topping) result = true;
                else continue;
            }
            return result;
        }
    }
}
