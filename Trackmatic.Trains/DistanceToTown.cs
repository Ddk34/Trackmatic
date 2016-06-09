using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Trains
{
    public class DistanceToTown
    {
        public int Distance { get; private set; }
        public RouteNode Node { get; private set; }

        public DistanceToTown(int distance, RouteNode node)
        {
            Distance = distance;
            Node = node;
        }

        public override string ToString()
        {
            return string.Concat(Node.Town, Distance);
        }
    }
}
