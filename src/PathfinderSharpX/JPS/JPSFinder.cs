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
        private Queue<Point> jumpPointsToTest = new Queue<Point>(); 

        public JPSFinder()
        {

        }

        public List<Point> FindPath(SearchParameters2D searchParameters, SearchMap2D map)
        {
            throw new NotImplementedException();
        }


        public List<Point> TestJumpPoints(SearchParameters2D searchParameters, SearchMap2D map)
        {
            var jumpPoints = new List<Point>();

            jumpPoints.Add(searchParameters.StartPoint);

            jumpPointsToTest.Enqueue(searchParameters.StartPoint);

            while (jumpPointsToTest.Count > 0)
            {
                var testPoint = jumpPointsToTest.Dequeue();
                foreach (var sector in Helpers.ScanSectors())
                {
                    ScanSector(testPoint, searchParameters.EndPoint, sector, map, jumpPoints);
                }
            }

            return jumpPoints;
        } 


        public bool ScanSector(Point currentPoint, Point endPoint, Direction scanDirection, SearchMap2D map, List<Point> jumpPoints)
        {
            if (map.IsBlockedByPinch(currentPoint, scanDirection))
            {
                return true;
            }
            if (!map.IsPointTraversable(currentPoint.NextPoint(scanDirection)))
            {
                return true;
            }

            if (LookAhead(currentPoint, currentPoint, Helpers.ScanDirections(scanDirection)[0], map, jumpPoints) ||
                LookAhead(currentPoint, currentPoint, Helpers.ScanDirections(scanDirection)[1], map, jumpPoints))
            {
                map.Nodes[currentPoint.X, currentPoint.Y].State = NodeState.Open;
            }
            else
            {
                map.Nodes[currentPoint.X, currentPoint.Y].State = NodeState.Closed;
            }

            return ScanSector(currentPoint.NextPoint(scanDirection), endPoint, scanDirection, map, jumpPoints);
        }


        private bool LookAhead(Point currentPoint, Point endPoint, Direction direction, SearchMap2D map, List<Point> jumpPoints)
        {
            if (!map.IsPointTraversable(currentPoint.NextPoint(direction)))
            {
                return false;
            }
            if (CheckForJumpPoint(currentPoint, endPoint, direction, map, jumpPoints))
            {
                return true;
            }

            return LookAhead(currentPoint.NextPoint(direction), endPoint, direction, map, jumpPoints);
        }

        private bool CheckForJumpPoint(Point point, Point endPoint, Direction direction, SearchMap2D map, List<Point> jumpPoints)
        {
            switch (direction)
            {
                case North:
                case South:
                    if (map.IsValidNeighbor(point, direction, East) || map.IsValidNeighbor(point, direction, West) || point == endPoint)
                    {
                        if (map.GetNode(point).State == NodeState.Untested)
                        {
                            jumpPoints.Add(point);
                        }                        
                        return true;
                    }
                    return false;
                case East:
                case West:
                    if (map.IsValidNeighbor(point, direction, North) || map.IsValidNeighbor(point, direction, South) || point == endPoint)
                    {
                        jumpPoints.Add(point);
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
            
        }
    }
}
