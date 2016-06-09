using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Trains
{
    public class RouteNode
    {
        private Dictionary<Town, DistanceToTown> _destinations;
        public Town Town { get; private set; }
        public RouteNode(Town town)
        {
            Town = town;
            _destinations = new Dictionary<Town, DistanceToTown>();
        }

        public void Add(RouteNode destination, int distance)
        {
            _destinations.Add(destination.Town, new DistanceToTown(distance, destination));
        }

        public List<ExtendedRoute> Traverse(ExtendedRoute extendedRoute, Func<ExtendedRoute, TraverseType> predicate)
        {
            Queue<KeyValuePair<ExtendedRoute, RouteNode>> nodes = new Queue<KeyValuePair<ExtendedRoute, RouteNode>>();
            nodes.Enqueue(new KeyValuePair<ExtendedRoute, RouteNode>(extendedRoute, this));

            return Traverse(nodes, predicate).ToList();
        }

        private IEnumerable<ExtendedRoute> Traverse(Queue<KeyValuePair<ExtendedRoute, RouteNode>> nodes, Func<ExtendedRoute, TraverseType> predicate)
        {
            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                foreach (var townRoute in node.Value._destinations.Values) //breadth 1st
                {
                    var clone = node.Key.Clone();
                    clone.Add(townRoute.Node.Town, townRoute.Distance);

                    var result = predicate(clone);

                    if (result == TraverseType.Stop)
                        continue;
                    else if (result == TraverseType.Return)
                        yield return clone;
                    else if (result == TraverseType.Continue)
                    {
                        nodes.Enqueue(new KeyValuePair<ExtendedRoute, RouteNode>(clone, townRoute.Node));
                    }
                    else if (result == TraverseType.ReturnAndContinue)
                    {
                        yield return clone;
                        nodes.Enqueue(new KeyValuePair<ExtendedRoute, RouteNode>(clone, townRoute.Node));
                    }
                }
            }
        }

        public override int GetHashCode()
        {
            return this.Town.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RouteNode);
        }

        public bool Equals(Town other)
        {
            return this.Town.Equals(other);
        }

        public bool Equals(RouteNode other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Equals(other.Town);
        }

        public override string ToString()
        {
            return string.Concat(Town, ">", string.Join("", _destinations.Values.Select(v => v.ToString())));
        }
    }
}
