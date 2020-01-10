using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation.Geography
{
    public class GeoPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
