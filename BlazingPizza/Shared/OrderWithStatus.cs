using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazingPizza.ComponentsLibrary.Maps;

namespace BlazingPizza.Shared
{
    public class OrderWithStatus
    {
        const string Preparing = "Preparing";
        const string OutForDelivery = "On the way";
        const string Delivered = "Delivered";

        public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(5);
        public readonly static TimeSpan DeliveryDuration = TimeSpan.FromSeconds(15);

        public Order MyOrder { get; set; }
        public string StatusText { get; set; }
        public bool IsDelivered => StatusText == Delivered;
        public List<Marker> MapMarkers { get; set; }
        public static OrderWithStatus FromOrder(Order order)
        {
            string message;
            List<Marker> markers;

            DateTime dispachTime = order.CreatedTime.Add(PreparationDuration);

            if (DateTime.Now < dispachTime)
            {
                message = Preparing;
                markers = new List<Marker>()
                {
                    ToMapMarker("You are here", order.DeliveryLocation, showPopup: true)
                };
            }
            else if (DateTime.Now < dispachTime+DeliveryDuration)
            {
                message = OutForDelivery;
                LatLong StartPosition = ComputeStartPosition(order);
                double ProportionOfDeliveryCompleted = Math.Min(1, (DateTime.Now - dispachTime).TotalMilliseconds / DeliveryDuration.TotalMilliseconds);
                LatLong DriverPosition = LatLong.Interpolate(StartPosition, order.DeliveryLocation, ProportionOfDeliveryCompleted);
                markers = new List<Marker>
                {
                    ToMapMarker("You are here", order.DeliveryLocation, showPopup: false),
                    ToMapMarker("You are here", DriverPosition, showPopup: false)
                };
            }
            else
            {
                message = Delivered;
                markers = new List<Marker>
                {
                    ToMapMarker("Delivered", order.DeliveryLocation, showPopup: true)
                };
            }
            return new OrderWithStatus
            {
                MyOrder = order,
                StatusText = message,
                MapMarkers = markers
            };
        }

        private static LatLong ComputeStartPosition(Order order)
        {
            Random random = new Random(order.OrderId);
            double distance = 0.01 + random.NextDouble() * 0.02;
            double angle = random.NextDouble() * Math.PI * 2;
            (double x, double y) offset = (distance * Math.Cos(angle), distance * Math.Sin(angle));
            return new LatLong(order.DeliveryLocation.Latitude + offset.x, order.DeliveryLocation.Longitude + offset.y);

        }

        private static Marker ToMapMarker(string description, LatLong coords, bool showPopup) => new Marker
        {
            Description = description,
            ShowPopup = showPopup,
            X = coords.Longitude,
            Y = coords.Latitude
        };
        
    }
}

