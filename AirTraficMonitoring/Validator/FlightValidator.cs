using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Validator
{
    public class FlightValidator : IValidator
    {


        public bool Validate(IAirspace airspace, ITrack track)
        {
            if (track.XPosition <= 0 || track.XPosition >= airspace.Width)
            {
                return false;
            }

            if (track.YPosition <= 0 || track.YPosition >= airspace.Height)
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

