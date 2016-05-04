using System;
using System.Collections.Generic;
using System.Linq;
using PathfinderSharpX.Commons;
using PathfinderSharpX.Utils;

namespace PathfinderSharpX.AStar
{
    public class AStarFinder
    {
        private Node2D startNode;
        private Node2D endNode;

        public bool TestMode { get; set; }

        public List<Point> AllNodesTested { get; set; }

        public AStarFinder()
        {
            AllNodesTested = new List<Point>();
        }


        public List<Point> FindPath(SearchParameters2D searchParameters)
        {
            startNode = searchParameters.StartNode;
            startNode.State = NodeState.Open;
            endNode = searchParameters.EndNode;

            var path = new List<Point>();

            var success = Search(startNode, searchParameters);
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

        private bool Search(Node2D currentNode, SearchParameters2D searchParameters)
        {
            currentNode.State = NodeState.Closed;
            var nextNodes = NodeOperations.GetAdjacentTraverableNodes(currentNode, searchParameters);
            nextNodes.Sort((n1, n2) => n1.F.CompareTo(n2.F));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location == endNode.Location) return true;

                if (TestMode)
                {
                    AllNodesTested.Add(new Point(nextNode.Location.X, nextNode.Location.Y));
                }

                if (Search(nextNode, searchParameters)) return true;
            }

            return false;
        }
    }
}