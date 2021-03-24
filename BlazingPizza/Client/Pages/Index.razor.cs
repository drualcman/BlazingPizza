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

        List<PizzaSpecial> Specials;
        bool ShowingConfigureDialog;
        Pizza ConfiguringPizza;

        protected override async Task OnInitializedAsync()
        {
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
            ShowingConfigureDialog = false;
        }
    }
}
