using Microsoft.VisualStudio.TestTools.UnitTesting;
using PierwszyProjekt.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.Algorithms.Tests
{
    [TestClass()]
    public class CircleCreatorTests
    {
        [TestMethod()]
        public void CreateCircleTest()
        {
            CircleCreator cc = new CircleCreator(99, 99);
        }
    }
}