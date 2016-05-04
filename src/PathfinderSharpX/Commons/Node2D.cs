using System;

namespace PathfinderSharpX.Commons
{
    public class Node2D
    {
        private Node2D parentNode;

         public Point Location { get; set; }

        public bool IsTraversable { get; set; }

        // Cost from start node
        public float G { get; set; }

        //straight line cost from this node to end
        public float H { get; set; }

        //estimated total cost to move
        public float F { get { return G + H; } }

        public Node2D ParentNode
        {
            get { return parentNode; }
            set
            {
                parentNode = value;
                G = parentNode.G + GetTraversalCost(Location, parentNode.Location);
            }
        }

        public NodeState State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isTraversable"></param>
        /// <param name="destination"></param>
        public Node2D(int x, int y, bool isTraversable, Point destination)
        {
            Location = new Point(x, y);
            IsTraversable = isTraversable;
            SetEndPoint(destination);
            State = NodeState.Untested;
            G = 0;
        }

        public void SetEndPoint(Point destination)
        {
            H = GetTraversalCost(Location, destination);
        }

        public static float GetTraversalCost(Point location, Point destination)
        {
            float deltaX = destination.X - location.X;
            float deltaY = destination.Y - location.Y;
            return (float) Math.Sqrt(deltaX*deltaX + deltaY*deltaY);
        }
    }

    public enum NodeState
    {
       Untested,
       Open,
       Closed 
    }
}