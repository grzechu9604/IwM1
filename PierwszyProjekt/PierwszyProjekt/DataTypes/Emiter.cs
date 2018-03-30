using System.Drawing;

namespace PierwszyProjekt.DataTypes
{
    public class Emiter
    {
        public Emiter(Point point, double angle)
        {
            Point = point;
            Angle = angle;
        }

        public Emiter(int x, int y, double angle)
        {
            Point = new Point(x, y);
            Angle = angle;
        }

        public Point Point { get; private set; }
        public double Angle { get; private set; }
    }
}
