using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PathfinderSharpX.Commons;

namespace PathfinderSharpX.JPS
{
    public class JPSFinder
    {
        private int width;
        private int height;
        private Node2D[,] nodes;
        private Node2D startNode;
        private Node2D endNode;
        private SearchParameters2D searchParameters;

        private List<Node2D> FindNaturalNeighbors(Node2D current)
        {
            var naturalNeighbors = new List<Node2D>();
            var adjacentNodes = new List<Node2D>();

            

            foreach (var node in adjacentNodes)
            {
                
                 
            }


            return naturalNeighbors;
        }
    }
}
