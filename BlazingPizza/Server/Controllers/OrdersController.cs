using BlazingPizza.Server.Models;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Server.Controllers
{
    [Route("orders")]
    [ApiController]
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
            order.DeliveryLocation = new LatLong(15.192927128168584, 120.58669395190812);       //my house now

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
                .OrderByDescending(o => o.CreatedTime)
                .ToListAsync();
            return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderWithStatus(int orderId)
        {
            IActionResult result;

            Order order = await Context.Orders.Where(o => o.OrderId == orderId)
                .Include(o => o.DeliveryLocation)
                .Include(o => o.Pizzas).ThenInclude(p => p.Special)
                .Include(o => o.Pizzas).ThenInclude(p => p.Toppings)
                    .ThenInclude(t => t.Topping)
                .SingleOrDefaultAsync();

            if (order is null) result = NotFound();
            else result = Ok(OrderWithStatus.FromOrder(order));

            return result;
        }
    }
}
