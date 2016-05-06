using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathfinderSharpX.AStar;
using PathfinderSharpX.Commons;
using PathfinderSharpX.JPS;

namespace PathfinderTests
{
    [TestClass]
    public class JPSTests
    {
        private bool[,] map;
        private SearchParameters2D searchParameters2D;
        private Point start;
        private Point destination;

        [TestInitialize]
        public void Initialize()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            map = new bool[7, 5];
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    map[x, y] = true;
                }
            }

            start = new Point(1, 2);
            destination = new Point(5, 2);
            searchParameters2D = new SearchParameters2D(start, destination);
        }

        private void AddWallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[4, 1] = false;
        }


        private void AddWallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path

            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[3, 0] = false;
        }

        [TestMethod]
        public void Test_WithoutWall_CanFindPath()
        {
            var pathfinder = new JPSFinder();

            var searchMap2D = new SearchMap2D(map);
            searchMap2D.SetDestination(destination);

            var path = pathfinder.FindPath(searchParameters2D, searchMap2D);

            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(4, path.Count);
        }

        [TestMethod]
        public void Test_WithOpenWall_CanFindPath_WithDiagonal()
        {
            AddWallWithGap();

            var searchMap2D = new SearchMap2D(map);
            searchMap2D.SetDestination(destination);

            var pathfinder = new JPSFinder();

            searchParameters2D.UseDiagonals = true;

            var path = pathfinder.FindPath(searchParameters2D, searchMap2D);

            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(5, path.Count);
        }

        [TestMethod]
        public void Test_WithOpenWall_CanFindPath_WithoutDiagonal()
        {
            AddWallWithGap();

            var searchMap2D = new SearchMap2D(map);
            searchMap2D.SetDestination(destination);

            var pathfinder = new JPSFinder();

            var path = pathfinder.FindPath(searchParameters2D, searchMap2D);


            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(8, path.Count);
        }

        [TestMethod]
        public void Test_WithClosedWall_NoPath()
        {
            AddWallWithoutGap();

            var searchMap2D = new SearchMap2D(map);
            searchMap2D.SetDestination(destination);

            var pathfinder = new JPSFinder();

            var path = pathfinder.FindPath(searchParameters2D, searchMap2D);


            Assert.IsNotNull(path);
            Assert.IsFalse(path.Any());
        }
    }
}