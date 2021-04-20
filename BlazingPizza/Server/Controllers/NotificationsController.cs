using BlazingPizza.Server.Models;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazingPizza.Server.Controllers
{
    [Route("notifications")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly PizzaStoreContext Context;

        public NotificationsController(PizzaStoreContext context)
        {
            this.Context = context;
        }

        [HttpPut("subscribe")]
        public async Task<NotificationSubscription> Subscribe(NotificationSubscription subscription)
        {
            string UserId = GetUserId();
            IQueryable<NotificationSubscription> oldSubscriptions = Context.NotificationSubscriptions.Where(e => e.UserId == UserId);
            Context.NotificationSubscriptions.RemoveRange(oldSubscriptions);
            subscription.UserId = UserId;
            Context.NotificationSubscriptions.Attach(subscription);
            await Context.SaveChangesAsync();
            return subscription;
        }


        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
