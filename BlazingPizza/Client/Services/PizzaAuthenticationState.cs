using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    /// <summary>
    /// Use persist the order
    /// </summary>
    public class PizzaAuthenticationState : RemoteAuthenticationState
    {
        public Order MyOrder { get; set; }
    }
}
