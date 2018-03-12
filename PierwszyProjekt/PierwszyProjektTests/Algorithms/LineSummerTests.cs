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
    public class LineSummerTests
    {
        [TestMethod()]
        public void LineSummerTest()
        {
            double[,] tab = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            List<Point> points = new List<Point>();
            points.Add(new Point(1, 1));
            points.Add(new Point(1, 2));
            points.Add(new Point(2, 1));
            points.Add(new Point(0, 1));
            LineSummer ls = new LineSummer(points, tab);

            Assert.AreEqual(ls.AmountOfPoints, points.ToArray().Length);
            Assert.AreEqual(ls.Average, (double)(5 + 6 + 8 + 2) / points.ToArray().Length);
            Assert.AreEqual(ls.Sum, 5 + 6+8+2);
        }
    }
}