using AirTraficMonitoring.Track;
using System;
using System.Collections.Generic;

namespace AirTraficMonitoring.Separation
{
    public class SeparationWarningEventArg : EventArgs
    {
        public List<ITrack> SeparationList { get; set; }
    }
}