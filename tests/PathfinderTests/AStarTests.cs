using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathfinderSharpX.AStar;
using PathfinderSharpX.Commons;

namespace PathfinderTests
{
    [TestClass]
    public class AStarTests
    {
        private bool[,] map;
        private SearchParameters2D searchParameters2D;

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

            searchParameters2D = new SearchParameters2D(new Point(1, 2), new Point(5, 2), map);
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
        public void Test_WihtoutWall_CanFindPath()
        {
            var pathfinder = new AStarFinder();

            var path = pathfinder.FindPath(searchParameters2D);

            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(4, path.Count);
        }

        [TestMethod]
        public void Test_WithOpenWall_CanFindPath()
        {
            AddWallWithGap();

            var pathfinder = new AStarFinder();

            var path = pathfinder.FindPath(searchParameters2D);

            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(5, path.Count);
        }

        [TestMethod]
        public void Test_WithClosedWall_NoPath()
        {
            AddWallWithoutGap();

            var pathfinder = new AStarFinder();

            var path = pathfinder.FindPath(searchParameters2D);


            Assert.IsNotNull(path);
            Assert.IsFalse(path.Any());
        }
    }
}
