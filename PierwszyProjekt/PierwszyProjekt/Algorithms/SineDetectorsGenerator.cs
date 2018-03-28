using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PierwszyProjekt.DataTypes;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class SineDetectorsGenerator : IDetectorsGenerator
    {
        public SineDetectorsGenerator(int amountOfDetectors, int angularSpread, Emiter emiter, int radius, Point centerOfThePoint)
        {
            Detectors = GenerateDetectors(amountOfDetectors, angularSpread, emiter, radius, centerOfThePoint);
        }

        private List<Detector> GenerateDetectors(int amountOfDetectors, int angularSpread, Emiter emiter, int radius, Point centerOfThePoint)
        {
            throw new NotImplementedException();
        }

        public List<Detector> Detectors { get; private set; }

    }
}
