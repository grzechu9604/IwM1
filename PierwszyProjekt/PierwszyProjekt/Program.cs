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
            int n = 100;
            CircleCreator cc = new CircleCreator(n-1, n-1);
            int[,] array = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i,j] = 0;
                }
            }

            cc.PointsOnCircle.ForEach(p => array[p.X, p.Y] = 1);
            LineCreator lc = new LineCreator(new Point(6, 6), new Point(54, 99));
            lc.Line.ForEach(p => array[p.X, p.Y] = 5);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }


        }
    }
}
