using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Trains
{
    public class Route
    {
        public Town Origin { get; private set; }
        public Town Destination { get; private set; }
        public int Distance { get; private set; }

        public Route(Town origin, Town destination, int distance)
        {
            if (origin == null)
                throw new ArgumentNullException("origin");

            if (destination == null)
                throw new ArgumentNullException("destination");

            if (distance < 1)
                throw new InvalidOperationException("Cant create a route with a distance between Towns less than 1.");

            if (origin.Equals(destination))
                throw new InvalidOperationException("Cant create a route where Origin and the Destination are the same.");

            Origin = origin;
            Destination = destination;
            Distance = distance;
        }

        public override int GetHashCode()
        {
            return Origin.GetHashCode() ^ Destination.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Route);
        }

        public bool Equals(Route other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Origin.Equals(other.Origin) &&
                Destination.Equals(other.Destination);
        }

        public override string ToString()
        {
            return string.Concat(Origin, Destination, Distance);
        }

        public static IEnumerable<Route> Parse(string data)
        {
            return Parse(data, ", ");
        }

        public static IEnumerable<Route> Parse(string data, string delimiter)
        {
            foreach(var route in data.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries))
            {
                yield return new Route(new Town(route[0]), new Town(route[1]), Int32.Parse(route.Substring(2, route.Length - 2)));
            }
        }
    }
}
