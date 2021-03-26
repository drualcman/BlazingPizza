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

        private int SelectedIndex = -1;
        public int SelectedValue
        {
            get => SelectedIndex;
            set => AddTopping(value);
        }

        protected override async Task OnInitializedAsync()
        {
            Toppings = await Client.GetFromJsonAsync<List<Topping>>("toppings");
        }

        void AddTopping(int id)
        {
            Topping topping = Toppings.Where(t => t.Id == id).First();            
            MyPizza.Toppings.Add(new PizzaTopping { Topping = topping });
            Toppings.Remove(topping);
        }

        void RemoveTopping(Topping topping)
        {
            MyPizza.Toppings.RemoveAll(pt => pt.Topping == topping);
            Toppings.Add(topping);
            Toppings.Sort((a, b) => a.Name.CompareTo(b.Name));
        }
    }
}
