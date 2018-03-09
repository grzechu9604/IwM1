using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PierwszyProjekt.Algorithms;

namespace PierwszyProjekt.DataTypes
{
    public class Line
    {
        public List<Point> Points { private set; get; }

        public Line(Point startPoint, Point targetPoint)
        {
            Points = new LineCreator(startPoint, targetPoint).Line;
        }
    }
}
