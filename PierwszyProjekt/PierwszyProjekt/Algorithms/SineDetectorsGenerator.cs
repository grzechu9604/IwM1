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
        public List<Detector> Detectors { get; private set; }

        public SineDetectorsGenerator(int amountOfDetectors, double angularSpread, Emiter emiter, int radius, Point centerOfTheCircle)
        {
            Detectors = GenerateDetectors(amountOfDetectors, angularSpread, emiter, radius, centerOfTheCircle);
        }

        private List<Detector> GenerateDetectors(int amountOfDetectors, double angularSpread, Emiter emiter, int radius, Point centerOfTheCircle)
        {
            List<Detector> detectors = new List<Detector>();
            double angle = emiter.Angle + Math.PI - CircleUtil.DegreesToRadians(angularSpread);
            double step = CircleUtil.DegreesToRadians(angularSpread / amountOfDetectors);

            for (int i = 0; i < amountOfDetectors; i++)
            {
                Point pointOfDetector = CircleUtil.GeneratePointOnArc(centerOfTheCircle, radius, angle);
                detectors.Add(new Detector(pointOfDetector));
                angle += step;
            }

            return detectors;
        }
    }
}
