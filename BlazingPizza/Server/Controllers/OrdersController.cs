using BlazingPizza.Server.Models;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using WebPush;

namespace BlazingPizza.Server.Controllers
{
    [Route("orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaStoreContext Context;

        public OrdersController(PizzaStoreContext context)
        {
            this.Context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
            order.CreatedTime = DateTime.Now;
            //order.DeliveryLocation = new LatLong(15.192927128168584, 120.58669395190812);       //my house now
            order.DeliveryLocation = new LatLong(15.192962600000001, 120.5866973);       //my house now from GPS
            order.UserId = GetUserId();
            foreach (Pizza pizza in order.Pizzas)
            {
                pizza.SpecialId = pizza.Special.Id;
                pizza.Special = null;

                foreach (PizzaTopping topping in pizza.Toppings)
                {
                    topping.ToppingId = topping.Topping.Id;
                    topping.Topping = null;
                }
            }
            Context.Add(order);
            await Context.SaveChangesAsync();
            NotificationSubscription subscription = await Context.NotificationSubscriptions.Where(e => e.UserId == GetUserId()).SingleOrDefaultAsync();
            if (subscription is not null) _ = TranckAndSendNotificationsAsync(order, subscription);
            return order.OrderId;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
        {
            List<Order> orders = await this.Context.Orders
                .Include(o => o.DeliveryLocation)
                .Include(o=> o.Pizzas).ThenInclude(p=>p.Special)
                .Include(o=> o.Pizzas).ThenInclude(p=> p.Toppings)
                                       .ThenInclude(t=> t.Topping)     
                .Where(u=>u.UserId == GetUserId())
                .OrderByDescending(o => o.CreatedTime)
                .ToListAsync();
            return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
        }

        [HttpGet("{orderid}")]
        public async Task<IActionResult> GetOrderWithStatus(int orderId)
        {
            IActionResult result;

            Order order = await Context.Orders.Where(o => o.OrderId == orderId && o.UserId == GetUserId())
                .Include(o => o.DeliveryLocation)
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings)
                    .ThenInclude(t => t.Topping)
                .SingleOrDefaultAsync();

            if (order is null) result = NotFound();
            else result = Ok(OrderWithStatus.FromOrder(order));

            return result;
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private static async Task SendNotificationAsync(Order order, NotificationSubscription subscription, string message)
        {
            // En una aplicación real puedes generar tus propias llaves en
            // https://tools.reactpwa.com/vapid
            string PublicKey =  "BLC8GOevpcpjQiLkO7JmVClQjycvTCYWm6Cq_a7wJZlstGTVZvwGFFHMYfXt6Njyvgx_GlXJeo5cSiZ1y4JOx1o";
            string PrivateKey = "OrubzSz3yWACscZXjFQrrtDwCKg-TGFuWhluQ2wLXDo";

            PushSubscription PushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
            // Aquí puedes colocar tu propio correo en someone@example.com
            VapidDetails VapidDetails = new VapidDetails("mailto:someone@example.com", PublicKey, PrivateKey);
            WebPushClient WebPushClient = new WebPushClient();
            try
            {
                string Payload = JsonSerializer.Serialize(new
                {
                    message,
                    url = $"myorders/{order.OrderId}",
                });
                await WebPushClient.SendNotificationAsync(PushSubscription, Payload, VapidDetails);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error al enviar la notificación push: {ex.Message}");
            }
        }

        private static async Task TranckAndSendNotificationsAsync(Order order, NotificationSubscription subscription)
        {
            await Task.Delay(OrderWithStatus.PreparationDuration);
            await SendNotificationAsync(order, subscription, "Your order is on the way!");
            await Task.Delay(OrderWithStatus.DeliveryDuration);
            await SendNotificationAsync(order, subscription, "Your order is delivered! Enjoy!");
        }
    }
}
