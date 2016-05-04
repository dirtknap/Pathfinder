using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSharpX.Commons
{
    public class SearchParameters2D
    {
        private Point endLocation;

        public Point StartLocation { get; set; }

        public Point EndLocation {
            get { return endLocation; }
            set
            {
                endLocation = value;
                SearchMap.SetDestination(value);
            }
        }

        public SearchMap2D SearchMap { get; }

        public Node2D StartNode { get { return SearchMap.Nodes[StartLocation.X, StartLocation.Y]; } }

        public Node2D EndNode { get { return SearchMap.Nodes[endLocation.X, endLocation.Y]; } }

        public bool UseDiagonals { get; set; }

        public bool DiagonalCornersBlock { get; set; }

        public SearchParameters2D(Point startLocation, Point endLocation, bool[,] map, bool useDiagonal = false, bool diagonalsBlockCorners = true)
        {
            SearchMap = new SearchMap2D(map);
            StartLocation = startLocation;
            EndLocation = endLocation;
            UseDiagonals = useDiagonal;
            DiagonalCornersBlock = true;       
        }


    }
}
