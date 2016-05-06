using System;
using System.Collections.Generic;
using PathfinderSharpX.Commons;

namespace PathfinderSharpX.Utils
{
    internal static class Helpers
    {
        private static Dictionary<Direction, Point> directionValues = new Dictionary<Direction, Point>
        {
            { Direction.North, new Point(1, 0) },
            { Direction.NorthEast, new Point(1, 1) },
            { Direction.East, new Point(0, 1) },
            { Direction.SouthEast, new Point(-1, 1) },
            { Direction.South, new Point(-1, 0) },
            { Direction.SouthWest, new Point(-1, -1) },
            { Direction.West, new Point(0, -1) },
            { Direction.NorthWest, new Point(1, -1) },
            { Direction.None, new Point(0, 0) }
        };

        internal static T[,] ModifyMap<T>(T[,] map, Func<int, int, T> m)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = m(x, y);
                }
            }
            return map;
        }

        internal static T[,] ModifyMapInplace<T>(T[,] map, Action<int, int> m)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    m(x, y);
                }
            }
            return map;
        }

        internal static Point DirectionValues(Direction direction)
        {
            return directionValues[direction];
        }
    }
}
