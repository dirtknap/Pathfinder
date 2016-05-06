using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSharpX.Commons
{
    public class SearchParameters2D
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public bool UseDiagonals { get; set; }

        public SearchParameters2D(Point startPoint, Point endPoint, bool useDiagonal = false)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            UseDiagonals = useDiagonal;
        }
    }
}
