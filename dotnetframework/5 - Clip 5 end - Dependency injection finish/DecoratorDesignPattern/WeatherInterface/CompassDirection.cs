using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class CompassDirection
    {

        private CompassDirection(string direction, string name, decimal minimumAzimuth, decimal middleAzimuth, decimal maximumAzimuth)
        {
            Abbreviation = direction;
            Name = name;
            MiddleAzimuth = middleAzimuth;
        }

        public String Abbreviation { get; private set; }

        public String Name { get; private set; }

        public decimal MiddleAzimuth { get; private set; }

        public decimal MinimumAzimuth { get; private set; }

        public decimal MaximumAzimuth { get; private set; }


        public static readonly CompassDirection NORTH = new CompassDirection("N", "North", 348.75m, 0.0m, 11.25m);

        public static readonly CompassDirection NORTH_NORTHEAST = new CompassDirection("NNE", "North-northeast", 11.25m, 22.5m, 33.75m);

        public static readonly CompassDirection NORTHEAST = new CompassDirection("NE", "Northeast", 33.75m, 45.0m, 56.25m);

        public static readonly CompassDirection EAST_NORTHEAST = new CompassDirection("ENE", "East-northeast", 56.25m, 67.5m, 78.75m);

        public static readonly CompassDirection EAST = new CompassDirection("E", "East", 78.75m, 90.0m, 101.25m);

        public static readonly CompassDirection EAST_SOUTHEAST = new CompassDirection("ESE", "East-southeast", 101.25m, 112.5m, 123.75m);

        public static readonly CompassDirection SOUTHEAST = new CompassDirection("SE", "Southeast", 123.75m, 135.0m, 146.25m);

        public static readonly CompassDirection SOUTH_SOUTHEAST = new CompassDirection("SSE", "South-southeast", 146.25m, 157.5m, 168.75m);

        public static readonly CompassDirection SOUTH = new CompassDirection("S", "South", 168.75m, 180.0m, 191.25m);

        public static readonly CompassDirection SOUTH_SOUTHWEST = new CompassDirection("SSW", "South-southwest", 191.25m, 202.5m, 213.75m);

        public static readonly CompassDirection SOUTHWEST = new CompassDirection("SW", "Southwest", 213.75m, 225.0m, 236.25m);

        public static readonly CompassDirection WEST_SOUTHWEST = new CompassDirection("WSW", "West-southwest", 236.25m, 247.5m, 258.75m);

        public static readonly CompassDirection WEST = new CompassDirection("W", "West", 258.75m, 270.0m, 281.25m);

        public static readonly CompassDirection WEST_NORTHWEST = new CompassDirection("WNW", "West-northwest", 281.25m, 292.5m, 303.75m);

        public static readonly CompassDirection NORTHWEST = new CompassDirection("NW", "Northwest", 303.75m, 315.0m, 326.25m);

        public static readonly CompassDirection NORTH_NORTHWEST = new CompassDirection("NNW", "North-northwest", 326.25m, 337.5m, 348.75m);

        public static readonly CompassDirection[] COMPASS_POINTS = {
                NORTH,
                EAST,
                SOUTH,
                WEST,
                NORTHEAST,
                SOUTHEAST,
                SOUTHWEST,
                NORTHWEST,
                NORTH_NORTHEAST,
            };



        public static CompassDirection GetDirection(decimal degrees)
        {
            var direction = COMPASS_POINTS
                .Where(p => degrees >= p.MinimumAzimuth && degrees <= p.MaximumAzimuth || degrees >= NORTH.MinimumAzimuth && degrees <= 360)
                .First();
            return direction;
        }


    }
}