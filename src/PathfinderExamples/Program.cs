using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PathfinderExamples.Examples;

namespace PathfinderExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new AStarMaze();
            var renderer = new DisplayRenderer();

            var result = maze.RunMaze();

            renderer.RenderResults(result);

            Console.ReadKey();
        }
    }
}
