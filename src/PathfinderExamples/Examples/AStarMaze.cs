using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderExamples.Commons;
using PathfinderSharpX.AStar;
using PathfinderSharpX.Commons;

namespace PathfinderExamples.Examples
{
    class AStarMaze
    {
        private SearchParameters2D searchParameters;
        private SearchMap2D searchMap;
        private AStarFinder pathfinder;
        private Point start = new Point(3, 40);
        private Point destination = new Point(76, 40);

        public AStarMaze()
        {
            InitializeMaze();
            pathfinder = new AStarFinder();
        }

        public int[,] RunMaze()
        {
            pathfinder.TestMode = true;
            searchParameters.UseDiagonals = true;
            var result = pathfinder.FindPath(searchParameters, searchMap);
            var testedNodes = pathfinder.AllNodesTested;
            return ConvertMapToResult(result, testedNodes);
        }

        private void InitializeMaze()
        {
            CreateMap();
            searchMap.SetDestination(destination);
            searchParameters = new SearchParameters2D(start, destination);
        }

        private void CreateMap()
        {
            var map = new bool[80, 80];
            var random = new Random();
            map = Utils.ModifyMap(map, (x, y) => random.Next(0,100) > 30);
            map[start.X, start.Y] = true;
            map[destination.X, destination.Y] = true;
            
            searchMap = new SearchMap2D(map);
        }

        private int[,] ConvertMapToResult(List<Point> path, List<Point> testedNodes )
        {
            var result = new int[searchMap.Width, searchMap.Height];

            result = Utils.ModifyMap(result, (x, y) => searchMap.Map[x, y] ? 0 : 1);

            foreach (var node in testedNodes)
            {
                result[node.X, node.Y] = 2;
            }

            foreach (var point in path)
            {
                result[point.X, point.Y] = 3;
            }

            result[start.X, start.Y] = 4;
            result[destination.X, destination.Y] = 5;

            return result;
        }
    }  
}
