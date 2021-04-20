using BlazingPizza.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Services
{
    public class OrdersClient
    {
        private readonly HttpClient Client;

        public OrdersClient(HttpClient client)
        {
            Client = client;
        }

        public async Task<List<OrderWithStatus>> GetOrders() =>
            await Client.GetFromJsonAsync<List<OrderWithStatus>>("orders");


        public async Task<OrderWithStatus> GetOrder(int orderId) =>
            await Client.GetFromJsonAsync<OrderWithStatus>($"orders/{orderId}");


        public async Task<int> PlaceOrder(Order order)
        {
            HttpResponseMessage response = await Client.PostAsJsonAsync("orders", order);
            response.EnsureSuccessStatusCode();
            int orderId = await response.Content.ReadFromJsonAsync<int>();
            return orderId;

        }

        public async Task SubscribeNotifications(NotificationSubscription subscription)
        {
            HttpResponseMessage response = await Client.PutAsJsonAsync("notifications/subscribe", subscription);
            response.EnsureSuccessStatusCode();
        }
    }
}
