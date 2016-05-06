using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PathfinderSharpX.Commons;
using PathfinderSharpX.Interfaces;
using PathfinderSharpX.Utils;
using static PathfinderSharpX.Commons.Direction;

namespace PathfinderSharpX.JPS
{
    public class JPSFinder : IPathfinder2D
    {
        private int width;
        private int height;
        private Node2D[,] nodes;
        private Node2D startNode;
        private Node2D endNode;
        private SearchParameters2D searchParameters;
        private SearchMap2D map;




        private List<Point> JumpPoints;

        public JPSFinder()
        {
            JumpPoints = new List<Point>();
        } 

        public List<Point> FindPath(SearchParameters2D searchParameters, SearchMap2D map)
        {
            throw new NotImplementedException();
        }

        private bool LookAhead(Point currentPoint, Direction direction)
        {
            throw new NotImplementedException();
        }

        private void CheckForForcedNeighbors(Point point, Direction direction )
        {
            switch (direction)
            {
                case North:
                case South:         
                    if (map.IsValidForcedNeighbor(point, direction, East))
                        { JumpPoints.Add(point.NextPoint(East, direction)); }
                    if (map.IsValidForcedNeighbor(point, direction, West))
                        { JumpPoints.Add(point.NextPoint(West, direction)); }
                    break;
                case East:
                case West:
                    if (map.IsValidForcedNeighbor(point, direction, North))
                    { JumpPoints.Add(point.NextPoint(North, direction)); }
                    if (map.IsValidForcedNeighbor(point, direction, South))
                    { JumpPoints.Add(point.NextPoint(South, direction)); }
                    break;
                case NorthEast:
                    if (map.IsValidForcedNeighbor(point, direction, North))
                        { JumpPoints.Add(point.NextPoint(North, direction)); }
                    if (map.IsValidForcedNeighbor(point, direction, East))
                        { JumpPoints.Add(point.NextPoint(East, direction)); }
                    break;
                case NorthWest:
                    if (map.IsValidForcedNeighbor(point, direction, North))
                    { JumpPoints.Add(point.NextPoint(North, direction)); }
                    if (map.IsValidForcedNeighbor(point, direction, West))
                    { JumpPoints.Add(point.NextPoint(South, direction)); }
                    break;
                case SouthEast:
                    if (map.IsValidForcedNeighbor(point, direction, South))
                        { JumpPoints.Add(point.NextPoint(East, direction)); }
                    if (map.IsValidForcedNeighbor(point, direction, East))
                        { JumpPoints.Add(point.NextPoint(South, direction)); }
                    break;
                case SouthWest:
                    if (map.IsValidForcedNeighbor(point, direction, South))
                        { JumpPoints.Add(point.NextPoint(North, direction)); }
                    if (map.IsValidForcedNeighbor(point, direction, West))
                        { JumpPoints.Add(point.NextPoint(South, direction)); }
                    break;
            }
        }
    }
}
