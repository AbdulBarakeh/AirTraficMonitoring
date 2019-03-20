using AirTraficMonitoring.FlightAirspace;
using AirTraficMonitoring.Track;

namespace AirTraficMonitoring.Validator
{
    public interface IValidator
    {
        bool Validate(IAirspace airspace, ITrack track);
    }
}