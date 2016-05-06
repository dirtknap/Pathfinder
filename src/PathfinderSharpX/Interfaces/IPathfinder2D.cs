using System.Collections.Generic;
using PathfinderSharpX.Commons;

namespace PathfinderSharpX.Interfaces
{
    public interface IPathfinder2D
    {
        List<Point> FindPath(SearchParameters2D searchParameters, SearchMap2D map);
    }
}