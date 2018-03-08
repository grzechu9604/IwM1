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
    public class DetectorsGeneratorTests
    {
        [TestMethod()]
        public void OppositPointTest1()
        {
            DetectorsGenerator dg = new DetectorsGenerator();
            Assert.AreEqual(new Point(7, 7), dg.OppositPoint(new Point(3, 3), 10, 10));
        }

        [TestMethod()]
        public void OppositPointTest2()
        {
            DetectorsGenerator dg = new DetectorsGenerator();
            Assert.AreEqual(new Point(3, 3), dg.OppositPoint(new Point(7, 7), 10, 10));
        }

        [TestMethod()]
        public void OppositPointTest3()
        {
            DetectorsGenerator dg = new DetectorsGenerator();
            Assert.AreEqual(new Point(6, 4), dg.OppositPoint(new Point(4, 6), 10, 10));
        }
    }
}