using System.Runtime.InteropServices;

namespace PathfinderSharpX.Commons
{
    public class SearchMap2D
    {
        public Node2D[,] Nodes { get; set; }
        public bool[,] Map { get; set; }
        public int Width { get { return Map.GetLength(0); } }
        public int Height { get { return Map.GetLength(1); } }


        public SearchMap2D(bool[,] map)
        {
            Map = map;
            InitializeNodes();
        }

        public void SetDestination(Point destination)
        {
            Nodes = Utils.ModifyMapInplace(Nodes, (x, y) => {
                Nodes[x, y].SetEndPoint(destination);
                Nodes[x,y].State = NodeState.Untested;
                });
        }

        public void UpdateMap(bool[,] map)
        {
            Nodes = Utils.ModifyMapInplace(Nodes, (x, y) =>
            {
                Nodes[x, y].IsTraversable = map[x, y];
                Nodes[x,y].State = NodeState.Untested;
            });
        }

        private void InitializeNodes()
        {
            Nodes = new Node2D[Width, Height];

            Nodes = Utils.ModifyMap(Nodes, (x, y) => new Node2D(x, y, Map[x,y], new Point()));
        }
    }
}