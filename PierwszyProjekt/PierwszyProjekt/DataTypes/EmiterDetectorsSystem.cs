﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.DataTypes
{
    public class EmiterDetectorsSystem
    {
        public Emiter Emiter {private set; get;}
        public List<Detector> Detectors { private set; get; }

        public EmiterDetectorsSystem(Emiter emiter, List<Detector> detectors)
        {
            Emiter = emiter;
            Detectors = detectors;
        }
    }
}