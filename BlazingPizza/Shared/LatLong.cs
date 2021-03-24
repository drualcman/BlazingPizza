using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Shared
{
    public class LatLong
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLong() { }
        public LatLong(double latitude, double longitude):this()            //call the default constructor to set a default process common always
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Calculate the interpolate between 2 points to show the point where we are
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static LatLong Interpolate(LatLong start, LatLong end, double proportion)
        {
            return new LatLong(
                start.Latitude + (end.Latitude - start.Latitude) * proportion,        //calculate latitude
                start.Longitude + (end.Longitude - start.Longitude) * proportion      //calculate longitude
                );
        }
    }
}
