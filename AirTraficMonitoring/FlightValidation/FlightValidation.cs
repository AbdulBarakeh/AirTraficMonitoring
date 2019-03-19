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
        public class FlightValidation
        {


            //public bool validation(IAirspace airSpace, ITrack track)
            //{

            //    if (((track.XPosition <= airSpace.Width) && (track.YPosition <= airSpace.Height)) &&
            //         ((track.Altitude <= airSpace.MaxAlt) && (track.Altitude => airSpace.MinAlt))
            //    {
            //        return true;
            //    }
            //else
            //{
            //        return false;
            //}

            //}
            public bool Validation(IAirspace airspace, ITrack track)
            {
                if (airspace.Width > track.XPosition)
                {
                    return false;
                }

                if (airspace.Height > track.YPosition)
                {
                    return false;
                }

                if (track.Altitude > airspace.MaxAlt)
                {
                    return false;
                }

                if (track.Altitude < airspace.MinAlt)
                {
                    return true;
                }

                
            }
        }
    }
}
