using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSharpX.Commons
{
    internal static class Utils
    {
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
    }
}
