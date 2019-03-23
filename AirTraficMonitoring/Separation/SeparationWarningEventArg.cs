using AirTraficMonitoring.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTraficMonitoring.Separation
{
    public class SeparationWarningEventArg : EventArgs
    {
        public List<ITrack> SeparationList { get; set; }
    }
}