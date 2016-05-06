using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderSharpX.Commons;

namespace PathfinderSharpX.Utils
{
    internal static class NodeOperations
    {

        internal static List<Node2D> GetAdjacentTraverableNodes(Node2D fromNode, SearchMap2D searchMap, bool useDiagonals)
        {
            var walkableNodes = new List<Node2D>();
            var width = searchMap.Width;
            var height = searchMap.Height;

            var nextLocations = GetAdjacentNodes(fromNode.Location, searchMap, useDiagonals);

            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;

                if (x < 0 || x >= width || y < 0 || y >= height) continue;

                var node = searchMap.Nodes[x, y];

                if (!node.IsTraversable || node.State == NodeState.Closed) continue;

                if (node.State == NodeState.Open)
                {
                    var traversalCost = Node2D.GetTraversalCost(node.Location, node.ParentNode.Location);
                    var gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        internal static IEnumerable<Point> GetAdjacentNodes(Point node, SearchMap2D searchMap, bool useDiagonals)
        {
            //  5 1 6
            //  0 □ 2
            //  4 3 7

            var result = new Point[(useDiagonals ? 8 : 4)];

            result[0] = new Point(node.X - 1, node.Y);
            result[1] = new Point(node.X, node.Y - 1);
            result[2] = new Point(node.X + 1, node.Y);
            result[3] = new Point(node.X, node.Y + 1);

            if (useDiagonals)
            {
                result[4] = new Point(node.X - 1, node.Y + 1);
                result[5] = new Point(node.X - 1, node.Y - 1);
                result[6] = new Point(node.X + 1, node.Y - 1);
                result[7] = new Point(node.X + 1, node.Y + 1);

                return CheckForPinchPoint(result, searchMap.Map);
            }

            return result;
        }

        private static IEnumerable<Point> CheckForPinchPoint(Point[] points, bool[,] map)
        {
            var remove = new bool[8];

            remove[4] = IsPinchPoint(points[3], points[0], map);
            remove[5] = IsPinchPoint(points[0], points[1], map);
            remove[6] = IsPinchPoint(points[1], points[2], map);
            remove[7] = IsPinchPoint(points[2], points[3], map);

            var toRemove = remove.Where(x => x == true).Count();

            var result = new Point[8 - toRemove];
            var resultCount = 0;

            for (int i = 0; i < remove.Length; i++)
            {
                if (!remove[i])
                {
                    result[resultCount] = points[i];
                    resultCount++;
                }
            }

            return result;
        }

        private static bool IsPinchPoint(Point a, Point b, bool[,] map)
        {
            try
            {
                return !map[a.X, a.Y] && !map[b.X, b.Y];
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }
    }
}
