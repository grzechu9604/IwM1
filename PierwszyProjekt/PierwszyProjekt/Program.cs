using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PierwszyProjekt.Algorithms;
using System.Drawing;

namespace PierwszyProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            CircleCreator cc = new CircleCreator(99, 99);
            int[,] array = new int[100, 100];
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    array[i,j] = 0;
                }
            }

            cc.PointsOnCircle.ForEach(p => array[p.X, p.Y] = 1);
            LineCreator lc = new LineCreator(new Point(6, 6), new Point(54, 99));
            lc.Line.ForEach(p => array[p.X, p.Y] = 5);

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }


        }
    }
}
