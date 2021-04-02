using BlazingPizza.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public Pizza ConfiguringPizza { get; private set; }
        public Order MyOrder { get; private set; } = new Order();

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
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

        public void Cancel()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }

        public void Confirm()
        {
            MyOrder.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }

        public void UpdatePizza(Pizza old, Pizza nueva)
        {
            MyOrder.Pizzas.Remove(old);
            MyOrder.Pizzas.Add(nueva);
        }

        public void RemovePizza(Pizza pizza)
        {
            MyOrder.Pizzas.Remove(pizza);
        }

        public void ResetOrder()
        {
            MyOrder = new Order();
        }
    }
}
