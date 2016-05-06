using System;
using System.Collections.Generic;
using System.Linq;
using PathfinderSharpX.Commons;
using PathfinderSharpX.Interfaces;
using PathfinderSharpX.Utils;

namespace PathfinderSharpX.AStar
{
    public class AStarFinder : IPathfinder2D
    {
        private Node2D startNode;
        private Node2D endNode;

        public bool TestMode { get; set; }

        public List<Point> AllNodesTested { get; set; }

        public AStarFinder()
        {
            AllNodesTested = new List<Point>();
        }


        public List<Point> FindPath(SearchParameters2D searchParameters, SearchMap2D map)
        {
            startNode = map.GetNode(searchParameters.StartPoint);
            startNode.State = NodeState.Open;
            endNode = map.GetNode(searchParameters.EndPoint);

            var path = new List<Point>();

            var success = Search(startNode, searchParameters, map);
            if (success)
            {
                var node = endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }
                path.Reverse();
            }

            return path;
        } 

        private bool Search(Node2D currentNode, SearchParameters2D searchParameters, SearchMap2D map)
        {
            currentNode.State = NodeState.Closed;
            var nextNodes = NodeOperations.GetAdjacentTraverableNodes(currentNode, map, searchParameters.UseDiagonals);
            nextNodes.Sort((n1, n2) => n1.F.CompareTo(n2.F));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location == endNode.Location) return true;

                if (TestMode)
                {
                    AllNodesTested.Add(new Point(nextNode.Location.X, nextNode.Location.Y));
                }

                if (Search(nextNode, searchParameters, map)) return true;
            }

            return false;
        }
    }
}