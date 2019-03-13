using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring
{
    namespace AirTraficMonitoring
    {
        public class FlightValidation : IAirspace
        {
            public bool validation()
            {
                if (ITrack.XPosition <= IAirspace.Width && ITrack.YPosition <= IAirspace.Height &&
                    ITrack.Altitude <= IAirspace.maxAlt && ITrack.Altitude => IAirspace.minAlt)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
