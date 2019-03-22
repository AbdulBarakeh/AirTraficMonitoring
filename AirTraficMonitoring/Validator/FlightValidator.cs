using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Validator
{
    public class FlightValidator : IValidator
    {

        #region Comment

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
        #endregion

        public bool Validate(IAirspace airspace, ITrack track)
        {
            if (0 <= track.XPosition && track.XPosition >= airspace.Width)
            {
                return false;
            }

            if (0 <= track.YPosition && track.YPosition >= airspace.Height)
            {
                return false;
            }

            if (track.Altitude > airspace.MaxAlt)
            {
                return false;
            }

            if (track.Altitude < airspace.MinAlt)
            {
                return false;
            }

            return true;
        }
    }
}

