using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PierwszyProjekt.DataTypes;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class EmiterGenerator
    {
        public EmiterGenerator(int amountOfEmiters, List<Point> circle)
        {
            Emiters = GenerateEmiters(amountOfEmiters, circle);
        }

        public List<Emiter> Emiters { private set; get; }

        private List<Emiter> GenerateEmiters(int amountOfEmiters, List<Point> circle)
        {
            int step = circle.ToArray().Length / amountOfEmiters;
            int amountOfPointsInFirstHalf = circle.ToArray().Length / 2;
            List<Emiter> emiters = new List<Emiter>();

            foreach (var point in circle.Take(amountOfPointsInFirstHalf).Where((p, i) => i % step == 0))
            {
                emiters.Add(new Emiter(point));
            }

            return emiters;
        }

    }
}
