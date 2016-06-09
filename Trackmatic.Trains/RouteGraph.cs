using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Trains
{
    public class RouteGraph
    {
        private Dictionary<Town, RouteNode> _townRoutes;

        public int Towns
        {
            get
            {
                return _townRoutes.Count;
            }
        }

        public RouteGraph(IEnumerable<Route> routes) :
            this()
        {
            foreach (var route in new HashSet<Route>(routes))
                Add(route);
        }

        public RouteGraph()
        {
            _townRoutes = new Dictionary<Town, RouteNode>();
        }

        public void Add(Route route)
        {
            var origin = GetOrAdd(route.Origin);
            var destination = GetOrAdd(route.Destination);

            origin.Add(destination, route.Distance);
        }

        private RouteNode GetOrAdd(Town town)
        {
            RouteNode routeNode = null;

            if (!_townRoutes.TryGetValue(town, out routeNode))
            {
                routeNode = new RouteNode(town);
                _townRoutes.Add(town, routeNode);
            }

            return routeNode;
        }

        public RouteNode this[Town town]
        {
            get
            {
                return _townRoutes[town];
            }
        }

        public List<ExtendedRoute> Traverse(Town town, Func<ExtendedRoute, TraverseType> predicate)
        {
            return this[town].Traverse(new ExtendedRoute(town), predicate);
        }

        public ExtendedRoute ShortestRoute(Town origin, Town destination)
        {
            ExtendedRoute shortestRoute = null;

            Func<ExtendedRoute, TraverseType> predicate = (ExtendedRoute er) =>
            {
                if (er.Stops > Towns)
                    return TraverseType.Stop; //This is to stop routes thats repeat on themselves

                if (er.EndsWith(destination))
                {
                    if (shortestRoute == null || shortestRoute?.Distance > er.Distance)
                        shortestRoute = er;

                    return TraverseType.Return;
                }

                return TraverseType.Continue;
            };

            var result = Traverse(origin, predicate);
            return shortestRoute;
        }

        public ExtendedRoute TryFindRoute(Town origin, Town destination, params Town[] destinations)
        {
            List<Town> route = new List<Town>() { origin, destination };
            route.AddRange(destinations ?? new Town[0]);

            return TryFindRoute(route);
        }

        public ExtendedRoute TryFindRoute(IEnumerable<Town> route)
        {
            Town origin = route.First();
            Town[] destinations = route.Skip(1).ToArray();

            bool found = false;

            Func<ExtendedRoute, TraverseType> predicate = (ExtendedRoute er) =>
            {
                if (found)
                    return TraverseType.Stop;

                if (er.IsBeginningOf(destinations))
                {
                    if (er.Contains(destinations))
                    {
                        found = true;
                        return TraverseType.Return;
                    }

                    return TraverseType.Continue;
                }

                return TraverseType.Stop;
            };

            var result = Traverse(origin, predicate);
            return result.FirstOrDefault();
        }

    }
}
