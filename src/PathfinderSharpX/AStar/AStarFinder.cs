using System;
using System.Collections.Generic;
using PathfinderSharpX.Commons;

namespace PathfinderSharpX.AStar
{
    public class AStarFinder
    {
        private int width;
        private int height;
        private Node2D[,] nodes;
        private Node2D startNode;
        private Node2D endNode;
        private SearchParameters2D searchParameters;

        public bool UseDiagonals { get; set; }

        
        public List<Point> FindPath(SearchParameters2D parameters, bool useDiagonal = false)
        {
            searchParameters = parameters;
            UseDiagonals = useDiagonal;
            InitializeNodes();
            startNode = nodes[searchParameters.StartLocation.X, searchParameters.StartLocation.Y];
            startNode.State = NodeState.Open;
            endNode = nodes[searchParameters.EndLocation.X, searchParameters.EndLocation.Y];

            var path = new List<Point>();

            var success = Search(startNode);
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

        private void InitializeNodes()
        {
            width = searchParameters.Map.GetLength(0);
            height = searchParameters.Map.GetLength(1);
            nodes = new Node2D[width,height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes[x,y] = new Node2D(x, y, searchParameters.Map[x,y], searchParameters.EndLocation);
                }
            }
        }

        private bool Search(Node2D currentNode)
        {
            currentNode.State = NodeState.Closed;
            var nextNodes = GetAdjacentTraverableNodes(currentNode);
            nextNodes.Sort((n1, n2) => n1.F.CompareTo(n2.F));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location == endNode.Location) return true;

                if (Search(nextNode)) return true;
            }

            return false;
        }

        private List<Node2D> GetAdjacentTraverableNodes(Node2D fromNode)
        {
            var walkableNodes = new List<Node2D>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;

                if (x < 0 || x >= width || y < 0 || y >= height) continue;

                var node = nodes[x, y];

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

        private IEnumerable<Point> GetAdjacentLocations(Point location)
        { 
            var result = new Point[(UseDiagonals ? 4 : 8)];

            result[0] = new Point(location.X + 1, location.Y);
            result[1] = new Point(location.X - 1, location.Y);
            result[2] = new Point(location.X, location.Y + 1);
            result[3] = new Point(location.X, location.Y - 1);

            if (UseDiagonals)
            {
                result[4] = new Point(location.X + 1, location.Y - 1);
                result[5] = new Point(location.X + 1, location.Y + 1);
                result[6] = new Point(location.X - 1, location.Y + 1);
                result[7] = new Point(location.X - 1, location.Y - 1);    
            }
            
            return result;
        }
    }
}