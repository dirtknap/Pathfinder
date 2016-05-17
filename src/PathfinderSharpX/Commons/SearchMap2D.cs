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

        public Node2D GetNode(Point point)
        {
            return Nodes[point.X, point.Y];
        }

        public bool IsPointTraversable(Point point)
        {
            if (point.X < 0 || point.X >= Width - 1 || point.Y < 0 || point.Y >= Width - 1)
            {
                return false;
            }
            return Map[point.X, point.Y];
        }

        public bool IsBlocked(Point origin, Direction direction)
        {
            if (origin.X < 0 || origin.X >= Width - 1 || origin.Y < 0 || origin.Y >= Width - 1)
            {
                return true;
            }
            return !IsPointTraversable(origin.NextPoint(direction));
        }

        public bool IsBlockedByPinch(Point origin, Direction direction)
        {
            switch (direction)
            {
                case Direction.NorthEast:
                    return IsBlocked(origin, Direction.North) && IsBlocked(origin, Direction.East);
                case Direction.NorthWest:
                    return IsBlocked(origin, Direction.North) && IsBlocked(origin, Direction.West);
                case Direction.SouthEast:
                    return IsBlocked(origin, Direction.South) && IsBlocked(origin, Direction.East);
                case Direction.SouthWest:
                    return IsBlocked(origin, Direction.South) && IsBlocked(origin, Direction.West);
                default:
                    return false;
            }
        }

        public bool IsValidNeighbor(Point point, Direction heading, Direction neighbor)
        {
            return IsBlocked(point, neighbor) && !IsBlocked(point, heading) && !IsBlocked(point.NextPoint(heading), neighbor);
        }


        public void SetDestination(Point destination)
        {
            Nodes = Utils.Helpers.ModifyMapInplace(Nodes, (x, y) => {
                Nodes[x, y].SetEndPoint(destination);
                Nodes[x,y].State = NodeState.Untested;
                });
        }

        public void UpdateMap(bool[,] map)
        {
            Nodes = Utils.Helpers.ModifyMapInplace(Nodes, (x, y) =>
            {
                Nodes[x, y].IsTraversable = map[x, y];
                Nodes[x,y].State = NodeState.Untested;
            });
        }

        private void InitializeNodes()
        {
            Nodes = new Node2D[Width, Height];

            Nodes = Utils.Helpers.ModifyMap(Nodes, (x, y) => new Node2D(x, y, Map[x,y], new Point()));
        }
    }
}