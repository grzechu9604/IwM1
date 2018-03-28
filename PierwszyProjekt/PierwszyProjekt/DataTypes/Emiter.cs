using System.Drawing;

namespace PierwszyProjekt.DataTypes
{
    public class Emiter
    {
        public Emiter(Point point)
        {
            Point = point;
        }

        public Emiter(int x, int y)
        {
            Point = new Point(x, y);
        }

        public Point Point { get; private set; }
    }
}
