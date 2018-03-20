using System.Collections.Generic;

namespace PierwszyProjekt.DataTypes
{
    public class EmiterDetectorsSystem
    {
        public Emiter Emiter { private set; get; }
        public List<Detector> Detectors { private set; get; }

        public EmiterDetectorsSystem(Emiter emiter, List<Detector> detectors)
        {
            Emiter = emiter;
            Detectors = detectors;
        }

        public Line GetLineForDetector(Detector d)
        {
            return new Line(Emiter.Point, d.Point);
        }
    }
}
