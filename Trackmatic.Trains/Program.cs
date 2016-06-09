using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Trains
{
    class Program
    {
        static void DisplaRouteDistance(ExtendedRoute route)
        {
            DisplayRouteInfo(route, (r) => r.Distance);
        }

        static void DisplayRouteInfo(ExtendedRoute route, Func<ExtendedRoute, object> infoToDisplay)
        {
            if (route == null)
                Console.WriteLine("NO SUCH ROUTE");
            else
                Console.WriteLine(infoToDisplay(route));
        }

        static void QuestionsOnetoFive(RouteGraph routeGraph)
        {
            DisplaRouteDistance(routeGraph.TryFindRoute("A","B","C"));
            DisplaRouteDistance(routeGraph.TryFindRoute("A", "D"));
            DisplaRouteDistance(routeGraph.TryFindRoute("A", "D", "C"));
            DisplaRouteDistance(routeGraph.TryFindRoute("A", "E", "B", "C", "D"));
            DisplaRouteDistance(routeGraph.TryFindRoute("A", "E", "D"));
        }

        static void QuestionSix(RouteGraph routeGraph)
        {
            Func<ExtendedRoute, TraverseType> predicate = (ExtendedRoute er) =>
            {
                if (er.Stops <= 3)
                {
                    if (er.EndsWith("C"))
                        return TraverseType.Return;

                    return TraverseType.Continue;
                }

                return TraverseType.Stop;
            };

            Console.WriteLine(routeGraph.Traverse("C", predicate).Count);
        }

        static void QuestionSeven(RouteGraph routeGraph)
        {
            Func<ExtendedRoute, TraverseType> predicate = (ExtendedRoute er) =>
            {
                if (er.Stops < 4)
                    return TraverseType.Continue;

                if (er.Stops == 4 && er.EndsWith("C"))
                    return TraverseType.Return;

                return TraverseType.Stop;
            };

            Console.WriteLine(routeGraph.Traverse("A", predicate).Count);
        }

        static void QuestionEightAndNine(RouteGraph routeGraph)
        {
            Console.WriteLine(routeGraph.ShortestRoute("A", "C").Distance);
            Console.WriteLine(routeGraph.ShortestRoute("B", "B").Distance);
        }

        static void QuestionTen(RouteGraph routeGraph)
        {
            Func<ExtendedRoute, TraverseType> predicate = (ExtendedRoute er) =>
            {
                if (er.Distance < 30)
                {
                    if (er.EndsWith("C"))
                        return TraverseType.ReturnAndContinue;

                    return TraverseType.Continue;
                }

                return TraverseType.Stop;
            };

            Console.WriteLine(routeGraph.Traverse("C", predicate).Count);
        }

        static void Main(string[] args)
        {
            var routeGraph = new RouteGraph(Route.Parse("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7"));

            QuestionsOnetoFive(routeGraph);
            QuestionSix(routeGraph);
            QuestionSeven(routeGraph);
            QuestionEightAndNine(routeGraph);
            QuestionTen(routeGraph);

            Console.ReadLine();
        }
    }
}
