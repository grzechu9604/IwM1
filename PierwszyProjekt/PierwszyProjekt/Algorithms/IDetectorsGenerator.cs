using PierwszyProjekt.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.Algorithms
{
    public interface IDetectorsGenerator
    {
        List<Detector> Detectors { get; }
    }
}
