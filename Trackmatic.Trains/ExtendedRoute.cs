using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Trains
{
    public class ExtendedRoute
    {
        private List<Town> _destinations;

        public Town Origin { get; private set; }
        public int Distance { get; private set; }

        public int Stops
        {
            get
            {
                return _destinations.Count;
            }
        }

        public ExtendedRoute(Town origin)
        {
            Origin = origin;
            _destinations = new List<Town>();
        }

        public void Add(Town destination, int distance)
        {
            Distance += distance;
            _destinations.Add(destination);
        }

        public bool Contains(Town[] route)
        {
            if (route.Length != _destinations.Count)
                return false;

            for(int i = 0; i < route.Length; i++)
            {
                if (!route[i].Equals(_destinations[i]))
                    return false;
            }

            return true;
        }

        public bool IsBeginningOf(Town[] route)
        {
            for (int i = 0; i < _destinations.Count; i++)
            {
                if (!route[i].Equals(_destinations[i]))
                    return false;
            }

            return true;
        }

        public bool EndsWith(Town town)
        {
            if (!_destinations.Any())
                return false;

            return _destinations.Last().Equals(town);
        }

        public ExtendedRoute Clone()
        {
            var clone = new ExtendedRoute(this.Origin);

            clone.Distance = this.Distance;

            foreach (var destinations in this._destinations)
                clone._destinations.Add(destinations);

            return clone;
        }
        public override string ToString()
        {
            return ToString(true);
        }

        public string ToString(bool withDistance)
        {
            return string.Concat(Origin, string.Join("", _destinations), withDistance ? Distance.ToString() : string.Empty);
        }
    }
}

