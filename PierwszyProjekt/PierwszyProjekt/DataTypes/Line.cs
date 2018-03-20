using PierwszyProjekt.Algorithms;
using System.Collections.Generic;
using System.Drawing;

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
