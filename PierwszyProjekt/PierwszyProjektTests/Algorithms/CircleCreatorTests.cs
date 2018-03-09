using Microsoft.VisualStudio.TestTools.UnitTesting;
using PierwszyProjekt.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.Algorithms.Tests
{
    [TestClass()]
    public class CircleCreatorTests
    {
        [TestMethod()]
        public void CreateCircleTest()
        {
            CircleCreator cc = new CircleCreator(99, 99);
            Point previousPoint = cc.PointsOnCircle.First();
            bool isNeighbour = true;

            cc.PointsOnCircle.Skip(1).ToList().ForEach(p =>
            {
                if (Math.Abs(previousPoint.X - p.X) > 1 && Math.Abs(previousPoint.Y - p.Y) > 1)
                {
                    isNeighbour = false;
                }
                previousPoint = p;
            });

            Assert.AreEqual(true, isNeighbour);
        }

        [TestMethod()]
        public void GeneratePointInSecoundQuarterOfCircleTest()
        {
            CircleCreator cc = new CircleCreator(9, 9);
            Assert.AreEqual(new Point(8, 1), cc.GeneratePointInSecoundQuarterOfCircle(new Point(1, 1)));
        }

        [TestMethod()]
        public void GeneratePointInThirdQuarterOfCircleTest()
        {
            CircleCreator cc = new CircleCreator(9, 9);
            Assert.AreEqual(new Point(8, 8), cc.GeneratePointInThirdQuarterOfCircle(new Point(1, 1)));
        }

        [TestMethod()]
        public void GeneratePointInForthQuarterOfCircleTest1()
        {
            CircleCreator cc = new CircleCreator(9, 9);
            Assert.AreEqual(new Point(1, 8), cc.GeneratePointInForthQuarterOfCircle(new Point(1, 1)));
        }
    }
}