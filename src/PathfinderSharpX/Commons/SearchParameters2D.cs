using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSharpX.Commons
{
    public class SearchParameters2D
    {
        public Point StartLocation { get; set; }

        public Point EndLocation { get; set; }

        public bool[,] Map { get; set; }

        public SearchParameters2D(Point startLocation, Point endLocation, bool[,] map)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Map = map;
        }
    }
}
