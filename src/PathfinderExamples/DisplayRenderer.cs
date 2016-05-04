using System;
using System.Collections.Generic;

namespace PathfinderExamples
{
    public class DisplayRenderer
    {
        private Dictionary<int, ConsoleColor> colorCode = new Dictionary<int, ConsoleColor>
        {
            {0, ConsoleColor.Black},
            {1, ConsoleColor.Cyan},
            {2, ConsoleColor.DarkGray},
            {3, ConsoleColor.Yellow},
            {4, ConsoleColor.Green },
            {5, ConsoleColor.Red }
        }; 

        public void RenderResults(int[,] resultMap)
        {
            Console.Clear();
            Console.SetWindowSize(resultMap.GetLength(0), resultMap.GetLength(1));
            Console.SetBufferSize(resultMap.GetLength(0), resultMap.GetLength(1));
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            for (int y = 0; y < resultMap.GetLength(1); y++)
            {
                for (int x = 0; x < resultMap.GetLength(0); x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = colorCode[resultMap[x, y]];
                    Console.Write(" ");
                }
            }

        }
    }
}