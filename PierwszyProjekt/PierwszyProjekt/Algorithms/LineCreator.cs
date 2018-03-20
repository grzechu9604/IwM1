using System.Collections.Generic;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class LineCreator
    {
        private Point p1;
        private Point p2;

        public List<Point> Line { get; private set; }

        public LineCreator(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;

            CreateBresenhamLine();
        }

        private void CreateBresenhamLine()
        {
            List<Point> points = new List<Point>();

            int d, dx, dy, ai, bi, xi, yi;
            int x = this.p1.X;
            int y = this.p1.Y;

            if (this.p1.X < this.p2.X)
            {
                xi = 1;
                dx = this.p2.X - this.p1.X;
            }
            else
            {
                xi = -1;
                dx = this.p1.X - this.p2.X;
            }

            if (this.p1.Y < this.p2.Y)
            {
                yi = 1;
                dy = this.p2.Y - this.p1.Y;
            }
            else
            {
                yi = -1;
                dy = this.p1.Y - this.p2.Y;
            }

            points.Add(new Point(x, y));

            if (dx > dy)
            {
                ai = (dy - dx) * 2;
                bi = dy * 2;
                d = bi - dx;

                while (x != this.p2.X)
                {
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        x += xi;
                    }

                    points.Add(new Point(x, y));
                }
            }
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;

                while (y != this.p2.Y)
                {
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        y += yi;
                    }

                    points.Add(new Point(x, y));
                }
            }

            this.Line = points;
        }
    }
}
