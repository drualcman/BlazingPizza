using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Shared
{
    /// <summary>
    /// Implement Browser Notifications
    /// This Properties is required can't change names
    /// </summary>
    public class NotificationSubscription
    {
        public int NotificationSubscriptionId { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }



    }
}
