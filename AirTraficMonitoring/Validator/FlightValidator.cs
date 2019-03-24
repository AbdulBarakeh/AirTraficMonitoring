using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Validator
{
    public class FlightValidator : IValidator
    {
        public bool Validate(IAirspace airspace, ITrack track)
        {
            var height = new
            {
                isValid = (track.XPosition <= airspace.Height && track.XPosition >= 0),
            };

            var width = new
            {
                isValid = (track.YPosition <= airspace.Width && track.YPosition >= 0),
            };

            var altitude = new
            {
                isValid = (track.Altitude <= airspace.MaxAlt && track.Altitude >= airspace.MinAlt),
            };

            return (height.isValid && width.isValid && altitude.isValid);
        }
    }
}

