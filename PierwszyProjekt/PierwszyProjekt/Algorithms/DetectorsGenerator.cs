using PierwszyProjekt.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class DetectorsGenerator
    {
        public DetectorsGenerator(int amountOfDetectors, List<Point> circle, Emiter emiter)
        {
            Detectors = GenerateDetectors(amountOfDetectors, circle, emiter);
        }

        public List<Detector> Detectors { private set; get; }

        private List<Detector> GenerateDetectors(int amountOfDetectors, List<Point> circle, Emiter emiter)
        {
            return new List<Detector>();
        }
    }
}
