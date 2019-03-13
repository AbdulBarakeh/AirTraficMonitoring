using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace AirTraficMonitoring
{
    public class FlightValidation : Airspace
    {
        public bool validation()
        {
            if (tesTrack.x <= Airspace.x && tesTrack.y <= Airspace.y)
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
