﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Shared
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public Address DeliveryAddress { get; set; } = new Address();
        public LatLong DeliveryLocation { get; set; }
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public decimal GetTotalPprice() => Pizzas.Sum(p => p.GetTotalPrice());
        public string GetFormattedTotalPrice() => GetTotalPprice().ToString("0.00");

    }
}
