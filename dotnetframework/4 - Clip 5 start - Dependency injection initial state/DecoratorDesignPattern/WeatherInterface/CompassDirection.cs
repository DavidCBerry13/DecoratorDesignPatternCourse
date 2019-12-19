using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.WeatherInterface
{

    /// <summary>
    /// Represents the directions on a 16 point compass
    /// </summary>
    public class CompassDirection
    {

        /// <summary>
        /// Creates a new CompassDirection.  This constructor is private as the only allowed CompassDirection objects are
        /// available as ReadOnly variables on this class
        /// </summary>
        /// <param name="direction">A string of the direction abbreviation (N, NE, NNE, E, etc)</param>
        /// <param name="name">A String of the direction name (North, Northeast, North-northeast, etc)</param>
        /// <param name="minimumAzimuth">The compass bearing in degrees of the midpoint of this direction</param>
        /// <param name="middleAzimuth">The minimum compass bearing in degrees that is considered this direction</param>
        /// <param name="maximumAzimuth">The maximum compass bearing in degrees that is considered this direction</param>
        private CompassDirection(string direction, string name, double minimumAzimuth, double middleAzimuth, double maximumAzimuth,
            int precedence, IsBearingInRangeDelegate isBearingInRange)
        {
            Abbreviation = direction;
            Name = name;
            MiddleAzimuth = middleAzimuth;
            MinimumAzimuth = minimumAzimuth;
            MaximumAzimuth = maximumAzimuth;
            IsBearingInRangeFunction = isBearingInRange;
            Precedence = precedence;
        }

        /// <summary>
        /// Gets the abbreaviation for the direction (ex: N, SE, ESE, etc)
        /// </summary>
        public string Abbreviation { get; private set; }

        /// <summary>
        /// Gets the full name of the direction (ex: North, Northeast, North-northeast)
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the midpoint of this direction in degrees (eg: 90 degrees for East)
        /// </summary>
        public double MiddleAzimuth { get; private set; }

        /// <summary>
        /// Gets the minimum degrees for the range of this direction
        /// </summary>
        public double MinimumAzimuth { get; private set; }

        /// <summary>
        /// Gets the maximum degrees for the range for this direction
        /// </summary>
        public double MaximumAzimuth { get; private set; }

        /// <summary>
        /// Integer indicating the precedence of this direction when determining the direction of a bearing.
        /// </summary>
        /// <remarks>
        /// There are bearings which are on the boundary between two directions.  For example, 11.25 degrees is the
        /// boundary between N and NNE.  So which should return.  This field allows you to specify one direction as the higher
        /// precedence, so it will return.  In this case, I want the cardinal directions (N, S, E, W) to have the highest precedence,
        /// then the 8 point directions (NE, SE, SW, NW) and finally the remaining directions.  This property allows you to do that.
        /// </remarks>
        public int Precedence { get; private set; }

        /// <summary>
        /// Function that does the work of checking if a bearing is in the range for this CompassDirection
        /// </summary>
        /// <remarks>
        /// This is implemented as a delegate so you can have different implementations of the check function.  This allows you
        /// to have a different function for North since it 'wraps around' from 348.75 to 11.25 degrees, so you can't do a simple
        /// min/max comparison.
        /// </remarks>
        internal IsBearingInRangeDelegate IsBearingInRangeFunction { get; set; }


        /// <summary>
        /// Checks if a given bearing in degrees is within this compass direction
        /// </summary>
        /// <param name="degrees">A bearing in degrees</param>
        /// <returns>True if the bearing is within this compass direction.  False otherwise</returns>
        public bool IsBearingInRange(double bearingInDegrees)
        {
            if (bearingInDegrees < 0 || bearingInDegrees > 360)
                throw new ArgumentException($"Degrees must be between 0 and 360 ({bearingInDegrees} was passed in)");

            return IsBearingInRangeFunction(bearingInDegrees, this);
        }


        public override bool Equals(object obj)
        {
            CompassDirection other = obj as CompassDirection;
            return Abbreviation.Equals(other.Abbreviation);
        }


        public override int GetHashCode()
        {
            return Abbreviation.GetHashCode();
        }

        /// <summary>
        /// Defines the delegate for the function to check if a bearing is in the rage of a compass direction
        /// </summary>
        /// <param name="degrees">A double of the bearing in degrees</param>
        /// <param name="direction">A Compass direction to check if the bearing is in the range of</param>
        /// <returns>True if the bearing is in rage of this direction.  False otherwise</returns>
        internal delegate bool IsBearingInRangeDelegate(double degrees, CompassDirection direction);


        static internal IsBearingInRangeDelegate StandardDirectionFunction = (degrees, direction)
            => degrees >= direction.MinimumAzimuth && degrees <= direction.MaximumAzimuth;

        static internal IsBearingInRangeDelegate NorthDirectionFunction = (degrees, direction)
            => degrees >= direction.MinimumAzimuth || degrees <= direction.MaximumAzimuth;



        public static readonly CompassDirection NORTH = new CompassDirection("N", "North", 348.75, 0.0, 11.25, 1, NorthDirectionFunction);

        public static readonly CompassDirection NORTH_NORTHEAST = new CompassDirection("NNE", "North-northeast", 11.25, 22.5, 33.75, 3, StandardDirectionFunction);

        public static readonly CompassDirection NORTHEAST = new CompassDirection("NE", "Northeast", 33.75, 45.0, 56.25, 2, StandardDirectionFunction);

        public static readonly CompassDirection EAST_NORTHEAST = new CompassDirection("ENE", "East-northeast", 56.25, 67.5, 78.75, 3, StandardDirectionFunction);

        public static readonly CompassDirection EAST = new CompassDirection("E", "East", 78.75, 90.0, 101.25, 1, StandardDirectionFunction);

        public static readonly CompassDirection EAST_SOUTHEAST = new CompassDirection("ESE", "East-southeast", 101.25, 112.5, 123.75, 3, StandardDirectionFunction);

        public static readonly CompassDirection SOUTHEAST = new CompassDirection("SE", "Southeast", 123.75, 135.0, 146.25, 2, StandardDirectionFunction);

        public static readonly CompassDirection SOUTH_SOUTHEAST = new CompassDirection("SSE", "South-southeast", 146.25, 157.5, 168.75, 3, StandardDirectionFunction);

        public static readonly CompassDirection SOUTH = new CompassDirection("S", "South", 168.75, 180.0, 191.25, 1, StandardDirectionFunction);

        public static readonly CompassDirection SOUTH_SOUTHWEST = new CompassDirection("SSW", "South-southwest", 191.25, 202.5, 213.75, 3, StandardDirectionFunction);

        public static readonly CompassDirection SOUTHWEST = new CompassDirection("SW", "Southwest", 213.75, 225.0, 236.25, 2, StandardDirectionFunction);

        public static readonly CompassDirection WEST_SOUTHWEST = new CompassDirection("WSW", "West-southwest", 236.25, 247.5, 258.75, 3, StandardDirectionFunction);

        public static readonly CompassDirection WEST = new CompassDirection("W", "West", 258.75, 270.0, 281.25, 1, StandardDirectionFunction);

        public static readonly CompassDirection WEST_NORTHWEST = new CompassDirection("WNW", "West-northwest", 281.25, 292.5, 303.75, 3, StandardDirectionFunction);

        public static readonly CompassDirection NORTHWEST = new CompassDirection("NW", "Northwest", 303.75, 315.0, 326.25, 2, StandardDirectionFunction);

        public static readonly CompassDirection NORTH_NORTHWEST = new CompassDirection("NNW", "North-northwest", 326.25, 337.5, 348.75, 3, StandardDirectionFunction);

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
                EAST_NORTHEAST,
                EAST_SOUTHEAST,
                SOUTH_SOUTHEAST,
                SOUTH_SOUTHWEST,
                WEST_SOUTHWEST,
                WEST_NORTHWEST,
                NORTH_NORTHWEST

            };


        /// <summary>
        /// Gets the CompassDirection object that correlates to the given bearing in degrees
        /// </summary>
        /// <param name="bearingInDegrees"></param>
        /// <returns></returns>
        public static CompassDirection GetDirection(double bearingInDegrees)
        {
            if (bearingInDegrees < 0 || bearingInDegrees > 360)
                throw new ArgumentException($"Degrees must be between 0 and 360 ({bearingInDegrees} was passed in)");


            var direction = COMPASS_POINTS
                .Where(d => d.IsBearingInRange(bearingInDegrees))
                .OrderBy(d => d.Precedence)
                .First();
            return direction;
        }



    }
}
